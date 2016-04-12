using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerAlert : MonoBehaviour
{
	public TextMesh text;

	private bool alertInProgress;
	private Queue<string> followers = new Queue<string>();	
	
	void Start () {
		Messenger.AddListener<string>(TwitchAlertsType.most_recent_follower.ToString(), OnNewFollower);
		text.text = "";
	}

	void OnNewFollower(string follower)
	{
		Debug.Log("New follower: " + follower);
		followers.Enqueue(follower);
	}

	void Update()
	{
		if (followers.Count > 0)
		{
			if (alertInProgress) return;

			StartCoroutine(Alert(followers.Dequeue()));
		}
	}

	IEnumerator Alert(string follower)
	{
		alertInProgress = true;
		text.text = follower + " just followed!";
		yield return new WaitForSeconds(3f);
		text.text = "";
		yield return new WaitForSeconds(0.5f);
		alertInProgress = false;		
	}
}
