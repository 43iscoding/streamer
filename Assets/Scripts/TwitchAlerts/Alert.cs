using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Alert : MonoBehaviour
{
	public TextMesh layoutText;

	[Header("Alert")]
	public TextMesh alertText;
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
		Messenger.AddListener<string>(Type().ToString(), OnAlert);
		alertText.text = "";
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
		SetLayoutText(data);
		StartCoroutine(ParticleCoroutine());
		if (sound && audioSource)
		{
			audioSource.PlayOneShot(sound);
		}
		SetContent(data);
		yield return new WaitForSeconds(duration);
		alertText.text = "";
		yield return new WaitForSeconds(1);
		alertInProgress = false;
	}

	protected void SetLayoutText(string message)
	{
		SetLayoutText(layoutText, message);
	}

	protected void SetLayoutText(TextMesh textMesh, string message)
	{
		if (textMesh == null) return;

		RotateLayoutText(textMesh, message);
	}

	void RotateLayoutText(TextMesh textMesh, string message)
	{
		iTween.RotateBy(textMesh.gameObject, new Hashtable()
		{
			{ "x", 0.25f },
			{ "time", 1 },
			{ "easetype", iTween.EaseType.easeInQuad },
			{ "oncomplete", "RotateLayoutText2" },
			{ "oncompleteparams", new LayoutTextParams()
			{
				text = textMesh,
				message = message
			} },
			{ "oncompletetarget", gameObject }
		});
	}

	void RotateLayoutText2(LayoutTextParams args)
	{
		args.text.text = args.message;
		args.text.transform.rotation = Quaternion.Euler(-90, 0, 0);
		iTween.RotateBy(args.text.gameObject, new Hashtable()
		{
			{ "x", 0.25f },
			{ "time", 1 },
			{ "easetype", iTween.EaseType.easeOutQuad },
			{ "oncomplete", "FinishRotate" },
			{ "oncompletetarget", gameObject }
		});
	}

	protected abstract void SetContent(string data);

	protected abstract TwitchAlertsType Type();

	class LayoutTextParams
	{
		public TextMesh text;
		public string message;
	}
}
