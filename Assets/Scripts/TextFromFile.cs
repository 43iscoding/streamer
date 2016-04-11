﻿using System;
using UnityEngine;
using System.IO;
using System.Text;

public class TextFromFile : MonoBehaviour
{
	public TextMesh text;

	public TwitchAlertsType type;

	private string pathBase = "C:\\Users\\User\\Desktop\\Twitch Alerts\\";
	private string extention = ".txt";

	public int updateFrequency = 1;

	private float lastUpdated;

	private string FilePath
	{
		get
		{
			return pathBase + type.ToString().Replace("thirty_", "30") + extention;
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
		if (text == null) return;

		lastUpdated = Time.realtimeSinceStartup;
		try
		{
			StreamReader reader = new StreamReader(FilePath, Encoding.Default);
			using (reader)
			{
				text.text = PostProcess(reader.ReadLine());
			}
			reader.Close();
		}
		catch (Exception e)
		{
			Debug.LogError(e);
		}
	}

	string PostProcess(string text)
	{
		if (text == null) return null;

		text = text.ToUpper();
		text = text.Replace("Â‚¬", ""); //Get rid of EUR sign
		return text;
	}
}