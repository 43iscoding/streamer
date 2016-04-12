using System;
using UnityEngine;
using System.Collections;
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
				writer.WriteLine("TestUser" + Random.Range(1, 999) + " ($" + Random.Range(1,100) + ".00)" + TextFromFile.DELIMETER + "This is a test donation Kappa Keepo Kappa");
			}
			writer.Close();
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
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
