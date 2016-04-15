using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TextMesh))]
public class TextMeshWrapper : MonoBehaviour
{
	private TextMesh textMesh;

	// Use this for initialization
	void Start ()
	{
		textMesh = GetComponent<TextMesh>();
		
	}

	public string text
	{
		get { return textMesh.text; }
		set { ProcessEmoticons(value); }
	}

	private void ProcessEmoticons(string text)
	{
		if (text == "")
		{
			textMesh.text = "";
			return;
		}

		string result = "";
		foreach (string word in text.Split(' '))
		{
			if (!TwitchEmotes.IsEmote(word))
			{
				result += word + " ";
			}
			else
			{
				result += string.Format("<quad material={0} x=0 y=0 width=1 height=1/> ", TwitchEmotes.GetIndex(word));
			}
		}

		textMesh.text = result;
	}
}
