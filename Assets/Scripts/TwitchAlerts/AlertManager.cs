using System.Collections.Generic;
using UnityEngine;

public class AlertManager : MonoBehaviour
{

	class AlertData
	{
		public TwitchAlertsType type;
		public string data;
	}

	private Queue<AlertData> queue = new Queue<AlertData>();

	public FollowerAlert[] followerAlerts;

	public DonationAlert[] donationsAlerts;

	public static bool alertInProgress;

	// Use this for initialization
	void Start () {
		Messenger.AddListener<string, bool>(TwitchAlertsType.most_recent_follower.ToString(), OnFollowerAlert);
		Messenger.AddListener<string, bool>(TwitchAlertsType.most_recent_donator.ToString(), OnDonationAlert);
	}

	void OnDonationAlert(string data, bool init)
	{
		if (init)
		{
			//SetLayoutText(data);
			return;
		}

		queue.Enqueue(new AlertData()
		{
			type = TwitchAlertsType.most_recent_donator,
			data = data
		});
	}

	void OnFollowerAlert(string data, bool init)
	{
		if (init)
		{
			//SetLayoutText(data);
			return;
		}

		queue.Enqueue(new AlertData()
		{
			type = TwitchAlertsType.most_recent_follower,
			data = data
		});
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
				Debug.LogError("No processor found for " + data.type + " | " + data.data);
				return;
			}

			processor.Process(data.data);
		}
	}

	Alert GetProcessor(AlertData data)
	{
		switch (data.type)
		{
			case TwitchAlertsType.most_recent_donator:
				return GetDonationAlertProcessor(data.data);
			case TwitchAlertsType.most_recent_follower:
				return GetFollowerAlertProcessor(data.data);
			default:
				return null;
		}
	}

	FollowerAlert GetFollowerAlertProcessor(string data)
	{
		return followerAlerts[0];
	}

	DonationAlert GetDonationAlertProcessor(string data)
	{
		return donationsAlerts[0];
	}
}
