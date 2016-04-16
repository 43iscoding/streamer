using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Random = UnityEngine.Random;

public class TwitchAlertsTest : MonoBehaviour
{
	private string pathBase = "C:\\Users\\User\\Desktop\\Twitch Alerts\\";
	private string extention = ".txt";

	public bool donationMode;

	public KeyCode key = KeyCode.F10;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(key))
		{
			if (donationMode)
			{
				FakeDonation();
			}
			else
			{
				FakeFollow();
			}
		}
	}

	private void FakeDonation()
	{
		string path = pathBase + TwitchAlertsType.most_recent_donator + extention;
		try
		{
			StreamWriter writer = new StreamWriter(path);
			using (writer)
			{
				writer.WriteLine("TestUser" + Random.Range(1, 999) + " ($" + Random.Range(1,100) + ".00)" + TextFromFile.DELIMETER + RandomDonationMessage());				
			}
			writer.Close();
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	private List<string> messages = new List<string>()
	{
		"Nice stream bro! Kappa Please play Darude Sandstorm dududu dududu dududu dududu dududu dududu"
	};

	private string RandomDonationMessage(int emotes = 0)
	{
		string message = messages[Random.Range(0, messages.Count - 1)];
		for (int i = 0; i < emotes; i++)
		{
			message += TwitchEmotes.RandomEmote() + " ";
		}
		return message;
	}

	private void FakeFollow()
	{
		string path = pathBase + TwitchAlertsType.most_recent_follower + extention;
		string countPath = pathBase + TwitchAlertsType.session_follower_count + extention;

		try
		{
			StreamWriter writer = new StreamWriter(path);
			using (writer)
			{
				writer.WriteLine("TestUser" + Random.Range(1, 999));
			}
			writer.Close();
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}
}
