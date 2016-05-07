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

	protected virtual void Start()
	{
		if (sprite)
		{
			sprite.enabled = false;
		}
		alertText.text = "";
	}

	protected virtual IEnumerator ParticleCoroutine()
	{
		if (!particleSystem) yield break;

		particleSystem.Play();

		yield return new WaitForSeconds(particlesDuration);

		particleSystem.Stop();
	}

	public void Process(AlertData data)
	{
		StartCoroutine(ProcessAlert(data));
	}

	protected virtual IEnumerator ProcessAlert(AlertData data)
	{
		AlertManager.alertInProgress = true;
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
		if (sprite)
		{
			sprite.enabled = false;
		}
		yield return new WaitForSeconds(1);
		AlertManager.alertInProgress = false;
	}

	protected virtual void SetLayoutText(AlertData data)
	{
		SetLayoutText(layoutText, data.username);
	}

	protected void SetLayoutText(TextMeshWrapper textMesh, string message)
	{
		if (textMesh == null) return;
		textMesh.text = message;
	}

	protected abstract void SetContent(AlertData data);

	protected abstract TwitchAlertsType Type();

	class LayoutTextParams
	{
		public TextMeshWrapper text;
		public string message;
	}
}
