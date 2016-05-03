using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WebcamController : MonoBehaviour
{

	public static string onSceneReload = "onSceneReload";

	public Material material;
	public RawImage image;

	private WebCamTexture webcamTexture;
	
	void Start () {
		Messenger.AddListener(onSceneReload, () =>
		{
			if (webcamTexture != null)
			{
				webcamTexture.Stop();
			}
		});
		webcamTexture = new WebCamTexture(1920, 1080);
		SetTexture();
		webcamTexture.Play();
	}

	void SetTexture()
	{
		if (material)
		{
			material.mainTexture = webcamTexture;
		}

		if (image)
		{
			image.texture = webcamTexture;
			image.material.mainTexture = webcamTexture;
		}
	}
		
	void Update ()
	{
		SetTexture();
	}
}
