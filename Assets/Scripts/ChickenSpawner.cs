using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawner : MonoBehaviour
{
	public GameObject chickenPrefab;
	public int numberOfChickens = 10;



	float minX = -3;
	float maxX = 3;



	private void Awake()
	{
		for (int i = 0; i < numberOfChickens; i++)
		{
			Instantiate(chickenPrefab, new Vector3(Random.Range(minX, maxX), 0.4f, 0), Quaternion.identity);
		}
	}
}
