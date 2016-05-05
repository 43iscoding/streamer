using UnityEngine;

public class FollowerAlert : Alert
{
	[Header("Follower")]
	public TextMeshWrapper followerCount;

	private int followers;

	protected override void Start()
	{
		base.Start();
		followers = int.Parse(TextFromFile.ReadOnce(TwitchAlertsType.session_follower_count));
		followerCount.text = followers.ToString();
	}

	protected override void SetContent(string data)
	{
		alertText.text = data + " just followed!";
		followers++;
		followerCount.text = followers.ToString();
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_follower;
	}
}
