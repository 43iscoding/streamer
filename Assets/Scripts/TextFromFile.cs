using System;
using UnityEngine;
using System.IO;
using System.Text;

public class TextFromFile : MonoBehaviour
{
	public TextMesh text;
	public string prepend = "";
	public string append = "";

	public TwitchAlertsType type;

	private string twitchAlertsPath = "C:\\Users\\User\\Desktop\\Twitch Alerts\\";
	private string deepBotPath = "D:\\Software\\Deepbot\\obs\\";
	private string extension = ".txt";

	public static readonly string DELIMETER = " %DELIMETER% ";

	public int updateFrequency = 1;

	private float lastUpdated;
	private string lastValue = null;

	private string FilePath
	{
		get
		{
			if (type.IsDeepBot())
			{
				return deepBotPath + type + extension;
			}
			else
			{
				return twitchAlertsPath + type.ToString().Replace("thirty_", "30") + extension;
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
		ReadFile();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (updateFrequency == 0) return;

		if (Time.realtimeSinceStartup - lastUpdated > updateFrequency)
		{
			ReadFile();
		}
	}

	void ReadFile()
	{
		lastUpdated = Time.realtimeSinceStartup;
		try
		{
			StreamReader reader = new StreamReader(FilePath, Encoding.Default);
			using (reader)
			{
				string line = reader.ReadLine();
				if (text != null)
				{
					text.text = PostProcess(line);
				}
				if (lastValue != line)
				{
					//Broadcast change
					if (Messenger.eventTable.ContainsKey(type.ToString()))
					{
						Messenger.Broadcast(type.ToString(), prepend + line + append, lastValue == null);
					}
				}
				lastValue = line;
			}
			reader.Close();
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	string PostProcess(string message)
	{
		if (message == null) return null;

		message = message.Replace("â‚¬", ""); //Get rid of EUR sign
		if (message.Contains(DELIMETER))
		{
			return message.Split(new [] { DELIMETER }, StringSplitOptions.None)[0];
		}
		return prepend + message + append;
	}	
}
