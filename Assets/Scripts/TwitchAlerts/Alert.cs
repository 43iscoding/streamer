using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Alert : MonoBehaviour
{
	public TextMesh text;	
	public float duration = 3f;	

	public AudioClip sound;

	public AudioSource audioSource;

	public ParticleSystem particleSystem;
	public float particlesDuration;

	private Queue<string> queue = new Queue<string>();

	protected static bool alertInProgress;

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

	protected IEnumerator ParticleCoroutine()
	{
		if (!particleSystem) yield break;

		particleSystem.Play();

		yield return new WaitForSeconds(particlesDuration);

		particleSystem.Stop();
	}

	protected virtual IEnumerator ProcessAlert(string data)
	{
		alertInProgress = true;
		StartCoroutine(ParticleCoroutine());
		if (sound && audioSource)
		{
			audioSource.PlayOneShot(sound);
		}
		SetContent(data);
		yield return new WaitForSeconds(duration);
		text.text = "";
		yield return new WaitForSeconds(1);
		alertInProgress = false;
	}

	protected abstract void SetContent(string data);

	protected abstract TwitchAlertsType Type();
}
