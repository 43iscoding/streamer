using System;
using UnityEngine;

public class DonationAlert : Alert
{
	public TextMesh message;

	protected override void Start()
	{
		base.Start();
		message.text = "";
	}

	protected override void SetContent(string data)
	{
		string[] entries = data.Split(new[] { TextFromFile.DELIMETER }, StringSplitOptions.None);

		string[] top = entries[0].Split('(');

		text.text = top[0] + "just donated " + top[1].Replace(")", "");
		message.text = entries[1];
		WindowsVoice.theVoice.Speak(message.text);
	}

	protected override void Cleanup()
	{
		message.text = "";
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_donator;
	}
}
