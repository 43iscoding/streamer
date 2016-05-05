using UnityEngine;

public class FollowerAlert : Alert
{
	[Header("Follower")]
	public TextMeshWrapper followerCount;

	protected override void Start()
	{
		base.Start();
		followerCount.text = TextFromFile.ReadOnce(TwitchAlertsType.session_follower_count);
	}

	protected override void SetContent(string data)
	{
		alertText.text = data + " just followed!";
		followerCount.text = TextFromFile.ReadOnce(TwitchAlertsType.session_follower_count);
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_follower;
	}
}
