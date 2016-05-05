using System;
using UnityEngine;
using System.IO;
using System.Text;

public class TextFromFile : MonoBehaviour
{
	public TextMesh text;
	public TextMeshWrapper textWrapper;
	public string prepend = "";
	public string append = "";

	public TwitchAlertsType type;

	private static string twitchAlertsPath = "C:\\Users\\User\\Desktop\\Twitch Alerts\\";
	private static string deepBotPath = "D:\\Software\\Deepbot\\obs\\";
	private static string extension = ".txt";

	public static readonly string DELIMETER = " %DELIMETER% ";

	public int updateFrequency = 1;

	private float lastUpdated;
	private string lastValue = null;

	static string GetFilePath(TwitchAlertsType type)
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

	// Use this for initialization
	void Start ()
	{
		Process();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (updateFrequency == 0) return;

		if (Time.realtimeSinceStartup - lastUpdated > updateFrequency)
		{
			Process();
		}
	}

	void Process()
	{
		lastUpdated = Time.realtimeSinceStartup;
		string line = ReadFile(GetFilePath(type));

		if (text != null)
		{
			text.text = PostProcess(line);
		}
		if (textWrapper != null)
		{
			textWrapper.text = PostProcess(line);
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

	static string ReadFile(string filePath)
	{
		try
		{
			StreamReader reader = new StreamReader(filePath, Encoding.Default);
			string line;
			using (reader)
			{
				line = reader.ReadLine();
			}
			reader.Close();
			return line;
		}
		catch (Exception e)
		{
			Debug.LogError(e);
			return "<ERROR>";
		}
	}

	public static string ReadOnce(TwitchAlertsType type)
	{
		return ReadFile(GetFilePath(type));
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
