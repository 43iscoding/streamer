﻿using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{
	private Queue<AlertData> queue = new Queue<AlertData>();

	public FollowerAlert[] followerAlerts;

	public DonationAlert[] donationsAlerts;

	public static bool alertInProgress;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<AlertData, bool>(TwitchAlertsType.most_recent_follower.ToString(), OnFollowerAlert);
		Messenger.AddListener<AlertData, bool>(TwitchAlertsType.most_recent_donator.ToString(), OnDonationAlert);
	}

	void OnDonationAlert(AlertData data, bool init)
	{
		if (init)
		{
			//SetLayoutText(data);
			return;
		}

		queue.Enqueue(data);
	}

	void OnFollowerAlert(AlertData data, bool init)
	{
		if (init)
		{
			//SetLayoutText(data);
			return;
		}

		queue.Enqueue(data);
	}

	void Update()
	{
		if (queue.Count > 0)
		{
			if (alertInProgress) return;

			AlertData data = queue.Dequeue();
			Alert processor = GetProcessor(data);
			if (processor == null)
			{
				Debug.LogError("No processor found for " + data.type + " | " + data.username);
				return;
			}

			processor.Process(data);
		}
	}

	Alert GetProcessor(AlertData data)
	{
		switch (data.type)
		{
			case TwitchAlertsType.most_recent_donator:
				DonationAlertData donationData = data as DonationAlertData;
				return GetDonationAlertProcessor(donationData.amount);
			case TwitchAlertsType.most_recent_follower:
				return GetFollowerAlertProcessor();
			default:
				return null;
		}
	}

	FollowerAlert GetFollowerAlertProcessor()
	{
		return followerAlerts[0];
	}

	DonationAlert GetDonationAlertProcessor(float amount)
	{
		return donationsAlerts[0];
	}
}
