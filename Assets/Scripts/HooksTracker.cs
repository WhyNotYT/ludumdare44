using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HooksTracker : MonoBehaviour
{
	
	public Hook[] hooks;
	private ChickenSpawner spawner;


	private void Start()
	{
		spawner = FindObjectOfType<ChickenSpawner>();
		hooks = FindObjectsOfType<Hook>();
	}
	void Update()
    {
		if(FindObjectOfType<PlayerController>().dead)
		{
			for (int i = 1; i < hooks.Length; i++)
			{
				Destroy(hooks[i].gameObject);
				hooks = FindObjectsOfType<Hook>();
			}
		}
		if (spawner.numberOfChickens < 3)
		{
			for (int i = 1; i < hooks.Length; i++)
			{
				Destroy(hooks[i].gameObject);
				hooks = FindObjectsOfType<Hook>();
			}
		}
		else if (spawner.numberOfChickens < 7)
		{
			for (int i = 2; i < hooks.Length; i++)
			{
				Destroy(hooks[i].gameObject);
				hooks = FindObjectsOfType<Hook>();
			}
		}
		else if (spawner.numberOfChickens < 10)
		{
			for (int i = 3; i < hooks.Length; i++)
			{
				Destroy(hooks[i].gameObject);
				hooks = FindObjectsOfType<Hook>();
			}
		}
		for (int i = 0; i < hooks.Length; i++)
		{
			for (int j = hooks.Length - 1; j > i; j--)
			{
				Debug.Log(i + ", " + j);
				if(hooks[i].targetChicken == hooks[j].targetChicken)
				{
					hooks[i].reTarget();
				}
			}
		}
    }
}
