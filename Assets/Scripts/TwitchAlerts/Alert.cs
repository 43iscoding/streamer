using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Alert : MonoBehaviour
{
	public TextMesh text;	
	public float duration = 3f;

	private Queue<string> queue = new Queue<string>();

	private static bool alertInProgress;

	protected virtual void Start()
	{
		Messenger.AddListener<string>(Type().ToString(), OnAlert);
		text.text = "";
	}

	void OnAlert(string data)
	{
		queue.Enqueue(data);
	}

	void Update()
	{
		if (queue.Count > 0)
		{
			if (alertInProgress) return;

			StartCoroutine(ProcessAlert(queue.Dequeue()));
		}
	}

	IEnumerator ProcessAlert(string data)
	{
		alertInProgress = true;
		SetContent(data);
		yield return new WaitForSeconds(duration);
		text.text = "";
		Cleanup();
		yield return new WaitForSeconds(2f);
		alertInProgress = false;
	}

	protected abstract void SetContent(string data);

	protected abstract TwitchAlertsType Type();

	protected virtual void Cleanup() { }
}
