using UnityEditor;
using UnityEngine;

public class BuildMenu
{

	private static string notificationsIconPath = "Assets/Sprites/Icons/Icon_Notifications.png";
	private static string streamerIconPath = "Assets/Sprites/Icons/Icon_Streamer.png";

	[MenuItem("Build/Notifications")]
	private static void BuildNotifications()
	{
		Debug.Log("Building Notifications");

		PlayerSettings.productName = "Notifications";

		EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length];
		System.Array.Copy(EditorBuildSettings.scenes, scenes, scenes.Length);
		scenes[0].enabled = false;
		scenes[1].enabled = false;
		scenes[2].enabled = true;
		EditorBuildSettings.scenes = scenes;

		Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(notificationsIconPath);
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Standalone, new[] { tex, tex, tex, tex, tex, tex, tex });
	}

	[MenuItem("Build/Streamer")]
	private static void BuildStreamer()
	{
		Debug.Log("Building Streamer");

		PlayerSettings.productName = "Streamer";

		EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length];
		System.Array.Copy(EditorBuildSettings.scenes, scenes, scenes.Length);
		scenes[0].enabled = true;
		scenes[1].enabled = true;
		scenes[2].enabled = false;
		EditorBuildSettings.scenes = scenes;

		Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(streamerIconPath);
		PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Standalone, new[] { tex, tex, tex, tex, tex, tex, tex });
	}
}
