using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleIntensityTune))]
public class ParticlePlayer : MonoBehaviour {

	private ParticleIntensityTune tuner;

	void Start () {
		tuner = GetComponent<ParticleIntensityTune>();
		Messenger.AddListener("LaunchSandstorm", () =>
		{
			StartCoroutine(Sandstorm());
		});
	}

	IEnumerator Sandstorm()
	{
		yield return new WaitForSeconds(2);
		tuner.Play();
		yield return new WaitForSeconds(7);
		tuner.Stop();
	}
}
