using UnityEngine;
using System.Collections;

public class ScaleTween : MonoBehaviour
{
	public float from = 0.9f;
	public float to = 1.1f;

	public float duration = 2f;

	public iTween.EaseType easeType = iTween.EaseType.easeInOutSine;

	// Use this for initialization
	void Start ()
	{
		transform.localScale = Vector3.one*from;
		ScaleTo();
	}

	void ScaleTo()
	{
		iTween.ScaleTo(gameObject, new Hashtable()
		{
			{ "scale", Vector3.one * to },
			{ "time", duration },
			{ "oncomplete", "ScaleFrom"},
			{ "easetype", easeType}
		});
	}

	void ScaleFrom()
	{
		iTween.ScaleTo(gameObject, new Hashtable()
		{
			{ "scale", Vector3.one * from },
			{ "time", duration },
			{ "oncomplete", "ScaleTo"},
			{ "easetype", easeType}
		});
	}
}
