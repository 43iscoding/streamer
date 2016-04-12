public class FollowerAlert : Alert
{
	protected override void SetContent(string data)
	{
		text.text = data + " just followed!";
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_follower;
	}
}
