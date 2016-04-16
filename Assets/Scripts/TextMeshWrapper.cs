using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class TextMeshWrapper : MonoBehaviour
{
	private TextMesh textMesh;
	public int charsPerLine = 80; 

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
		int length = 0;
		foreach (string word in text.Split(' '))
		{
			if (length > charsPerLine)
			{
				result += "\n";
				length = 0;
			}
			if (!TwitchEmotes.IsEmote(word))
			{
				result += word + " ";
				length += word.Length + 1;
			}
			else
			{
				Vector2 pos = TwitchEmotes.GetPos(word);
				const float offset = 0.1428f;
				result += string.Format("<quad material=1 x={0} y={1} width={2} height={3} size=80/> ", offset * pos.x, offset * pos.y, offset, offset);
				length += 4;
			}
		}

		textMesh.text = result;
	}
}
