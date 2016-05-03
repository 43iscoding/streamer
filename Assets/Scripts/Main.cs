using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//Application.Quit();
			return;
		}

		if (Input.GetKeyDown(KeyCode.F1))
		{
			Messenger.Broadcast(WebcamController.onSceneReload);
			SceneManager.LoadScene("StartingSoon");
			return;
		}

		if (Input.GetKeyDown(KeyCode.F2))
		{
			Messenger.Broadcast(WebcamController.onSceneReload);
			SceneManager.LoadScene("New");
			return;
		}
		
	}
}
