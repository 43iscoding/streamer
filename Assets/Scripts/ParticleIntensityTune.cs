using UnityEngine;
using System.Collections;

public class ParticleIntensityTune : MonoBehaviour
{
	public ParticleSystem particles;

	public static readonly float DEFAULT_RATE = 15f;

	public static readonly float MIN_RATE = 7f;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			ParticleSystem.EmissionModule emission = particles.emission;
			emission.rate = new ParticleSystem.MinMaxCurve(emission.rate.constantMax + 1);
		}

		if (Input.GetKeyDown(KeyCode.KeypadMinus))
		{
			ParticleSystem.EmissionModule emission = particles.emission;
			emission.rate = new ParticleSystem.MinMaxCurve(emission.rate.constantMax - 1);
		}
	}

	public void SetRate(float rate)
	{
		ParticleSystem.EmissionModule emission = particles.emission;
		emission.rate = new ParticleSystem.MinMaxCurve(Mathf.Max(MIN_RATE, rate));
	}
}
