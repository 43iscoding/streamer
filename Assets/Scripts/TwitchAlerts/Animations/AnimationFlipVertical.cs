using System.Collections;
using UnityEngine;

public class AnimationFlipVertical : AnimationProcessor {

	public override void Animate(TextMesh target, string value)
	{
		RotateLayoutText(target, value);
	}

	void RotateLayoutText(TextMesh textMesh, string message)
	{
		iTween.RotateBy(textMesh.gameObject, new Hashtable()
		{
			{ "x", 0.25f },
			{ "time", 1 },
			{ "easetype", iTween.EaseType.easeInQuad },
			{ "oncomplete", "RotateLayoutText2" },
			{ "oncompleteparams", new LayoutTextParams()
			{
				text = textMesh,
				message = message
			} },
			{ "oncompletetarget", gameObject }
		});
	}

	void RotateLayoutText2(LayoutTextParams args)
	{
		args.text.text = args.message;
		args.text.transform.rotation = Quaternion.Euler(-90, 0, 0);
		iTween.RotateBy(args.text.gameObject, new Hashtable()
		{
			{ "x", 0.25f },
			{ "time", 1 },
			{ "easetype", iTween.EaseType.easeOutQuad },
			{ "oncomplete", "FinishRotate" },
			{ "oncompletetarget", gameObject }
		});
	}

	class LayoutTextParams
	{
		public TextMesh text;
		public string message;
	}
}
