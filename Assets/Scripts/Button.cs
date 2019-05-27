/* Copyright (C) Why? Not! YT (Yumish R. Niroula) 2019
 * Ludum Dare 44 [Your Life is Currency]

 * Youtube Channel: https://www.youtube.com/channel/UC0AkLhy8aP8ns1QGD2lEIuw
 * itch.io: https://whynotyt.itch.io/

 * Contact e-mail: yumishniroula2002@gmail.com
*/


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Collider2D))]
public class Button : MonoBehaviour
{

	[SerializeField] private string SceneToLoad;
	[SerializeField] private GameObject LoadingScreen;

	private Vector3 OriginalScale;
	private bool Loading;
	private AsyncOperation LoadingProg;
	//private FileHandler file = new FileHandler();



	private void Start()
	{
		//file.OpenFile(file.DefPath);
		OriginalScale = this.transform.localScale;
		if (LoadingScreen != null)
		{
			LoadingScreen.SetActive(false);
		}
	}


	private void Update()
	{
		if (Loading)
		{
			LoadingScreen.GetComponentInChildren<Image>().fillAmount = LoadingProg.progress;
		}

	}

	private void OnMouseDown()
	{

		if (SceneToLoad == "Quit")
		{
			//file.Encrypt();
			Application.Quit();
		}
		else if (SceneToLoad != null)
		{
			if (LoadingScreen != null)
			{
				GameObject LoadingScreenClone = Instantiate(LoadingScreen, Vector3.zero, Quaternion.identity);

				LoadingScreenClone.SetActive(true);
				Loading = true;
			}
			LoadingProg = SceneManager.LoadSceneAsync(SceneToLoad);
		}
	}



	private void OnMouseEnter()
	{
		this.transform.localScale = OriginalScale * 1.1f;
	}


	private void OnMouseExit()
	{
		this.transform.localScale = OriginalScale;
	}
}
