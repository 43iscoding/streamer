using UnityEngine;
using System.Collections;

public class ProgressColor : MonoBehaviour
{
	public TextMesh text;

	public Color from = Color.red;
	public Color to = Color.green;

	public float target;

	private float value;
	
	// Update is called once per frame
	void Update ()
	{
		if (!text) return;

		if (!float.TryParse(text.text, out value))
		{
			Debug.LogError("Could not parse " + text.text + " - not a number");
		}
		text.color = Color.Lerp(from, to, value / target);
	}
}
