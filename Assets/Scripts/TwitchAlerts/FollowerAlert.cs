using System.Collections;
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

	protected override void SetContent(AlertData data)
	{
		//alertText.text = data.username + " just followed!";
		alertText.text = data.username;
		followers++;
		followerCount.text = followers.ToString();
	}

	protected override TwitchAlertsType Type()
	{
		return TwitchAlertsType.most_recent_follower;
	}

	protected override IEnumerator ParticleCoroutine()
	{
		if (particleSystem == null) yield break;

		particleSystem.Emit(1);
	}
}
