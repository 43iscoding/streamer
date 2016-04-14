using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DonationAlert : Alert
{
	public TextMesh message;

	private float averageWordsPerSecond = 2.166f;

	public ParticleIntensityTune particlesTuning;

	protected override void Start()
	{
		base.Start();
		message.text = "";
	}

	protected override IEnumerator ProcessAlert(string data)
	{
		alertInProgress = true;
		StartCoroutine(ParticleCoroutine());
		if (sound && audioSource)
		{
			audioSource.PlayOneShot(sound);
		}
		SetContent(data);
		StartCoroutine(VoiceCoroutine(message.text, sound.length));
		yield return new WaitForSeconds(duration);
		text.text = "";
		message.text = "";		
	}

	protected override void SetContent(string data)
	{
		string[] entries = data.Split(new[] { TextFromFile.DELIMETER }, StringSplitOptions.None);

		string[] top = entries[0].Split('(');

		if (particlesTuning)
		{
			string value = new string(top[1].Where(c => char.IsDigit(c) || c == '.').ToArray());
			float rate;
			if (float.TryParse(value, out rate))
			{
				particlesTuning.SetRate(rate);
			}
			else
			{
				particlesTuning.SetRate(ParticleIntensityTune.DEFAULT_RATE);
			}
		}

		text.text = top[0] + "just donated " + top[1].Replace(")", "");
		message.text = entries[1];
	}

	IEnumerator VoiceCoroutine(string toSpeak, float delay)
	{
		yield return new WaitForSeconds(delay);

		WindowsVoice.theVoice.Speak(toSpeak);
		yield return new WaitForSeconds(EstimateDuration(toSpeak) + 1);
		alertInProgress = false;
	}

	float EstimateDuration(string toSpeak)
	{
		int words = toSpeak.Split(' ').Length;
		float estimation = words / averageWordsPerSecond;
		Debug.Log(toSpeak + " (" + words + ") -> " + estimation);
		return estimation;
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_donator;
	}
}
