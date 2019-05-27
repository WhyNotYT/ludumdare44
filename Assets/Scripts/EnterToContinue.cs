using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EnterToContinue : MonoBehaviour
{
	public string SceneToLoad;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(SceneToLoad);
		}
    }
}
