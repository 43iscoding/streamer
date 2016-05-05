using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Alert : MonoBehaviour
{
	public TextMeshWrapper layoutText;

	[Header("Alert")]
	public TextMesh alertText;
	public SpriteRenderer sprite;
	public float duration = 3f;

	[Header("Sound")]
	public AudioClip sound;

	public AudioSource audioSource;

	[Header("Particles")]
	public ParticleSystem particleSystem;
	public float particlesDuration;

	private Queue<string> queue = new Queue<string>();

	protected static bool alertInProgress;

	protected virtual void Start()
	{
		Messenger.AddListener<string, bool>(Type().ToString(), OnAlert);
		if (sprite)
		{
			sprite.enabled = false;
		}
		alertText.text = "";
	}

	void OnAlert(string data, bool init)
	{
		if (init)
		{
			SetLayoutText(data);
			return;
		}
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

	protected virtual IEnumerator ParticleCoroutine()
	{
		if (!particleSystem) yield break;

		particleSystem.Play();

		yield return new WaitForSeconds(particlesDuration);

		particleSystem.Stop();
	}

	protected virtual IEnumerator ProcessAlert(string data)
	{
		alertInProgress = true;
		if (sprite)
		{
			sprite.enabled = true;
		}
		SetLayoutText(data);
		StartCoroutine(ParticleCoroutine());
		if (sound && audioSource)
		{
			audioSource.PlayOneShot(sound);
		}
		SetContent(data);
		yield return new WaitForSeconds(duration);
		alertText.text = "";
		alertInProgress = false;
		if (sprite)
		{
			sprite.enabled = false;
		}
		yield return new WaitForSeconds(1);
	}

	protected virtual void SetLayoutText(string message)
	{
		SetLayoutText(layoutText, message);
	}

	protected void SetLayoutText(TextMeshWrapper textMesh, string message)
	{
		if (textMesh == null) return;
		textMesh.text = message;
	}

	protected abstract void SetContent(string data);

	protected abstract TwitchAlertsType Type();

	class LayoutTextParams
	{
		public TextMeshWrapper text;
		public string message;
	}
}
