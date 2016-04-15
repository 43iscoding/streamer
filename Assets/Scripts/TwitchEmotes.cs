using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TwitchEmotes : MonoBehaviour
{
	public static TwitchEmotes instance;

	private static Dictionary<string, int> emotes;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		emotes = new Dictionary<string, int>
		{
			{"Kappa", 1},
			{"4Head", 2},
			{"BabyRage", 3},
		};
	}

	public static string RandomEmote()
	{
		return emotes.Keys.ToArray()[Random.Range(0, emotes.Keys.Count - 1)];
	}

	public static bool IsEmote(string word)
	{
		return emotes.ContainsKey(word);
	}

	public static int GetIndex(string word)
	{
		return emotes[word];
	}
}
