using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class DonationAlert : Alert
{
	[Header("Donation")]
	public TextMeshWrapper message;

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
		if (sound && audioSource)
		{
			audioSource.PlayOneShot(sound);
		}
		StartCoroutine(ParticleCoroutine());
		SetLayoutText(data, false);
		SetContent(data);
		
		yield return new WaitForSeconds(duration);
		alertText.text = "";
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

		alertText.text = top[0] + "just donated " + top[1].Replace(")", "");
		message.text = entries[1];
		StartCoroutine(VoiceCoroutine(entries[1], sound.length));
	}

	protected override void SetLayoutText(string message, bool instant)
	{
		message = message.Split(new[] { TextFromFile.DELIMETER }, StringSplitOptions.None)[0];
		message = message.Replace("â‚¬", ""); //Get rid of EUR sign
		SetLayoutText(layoutText, message, instant);
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
		return estimation;
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_donator;
	}
}
