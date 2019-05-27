using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
	public float Speed = 1;
	public float pullTimeMin = 3;
	public float pullTimeMax = 10;
	public Animator anim;
	public SpriteRenderer deadChickenSprite;
	public ChickenAI targetChicken;

	private PlayerController player;
	private bool Pulling;
	
	private float pullCounter;
	//private int numberOfChickens;
	private float currentPullTime;
	private float Timer = 99999999999;
	private bool pulled;
	private void Start()
	{
		player = FindObjectOfType<PlayerController>();
		//numberOfChickens = FindObjectsOfType<ChickenAI>().Length;
		reTarget();
	}

	private void Update()
	{
		if(Timer < Time.time)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
		}
		if (!player.dead)
		{
			//numberOfChickens = FindObjectOfType<ChickenSpawner>().numberOfChickens;
			if (pullCounter > Time.time)
			{
				this.transform.position = Vector3.Lerp(this.transform.position, targetChicken.transform.position + new Vector3(0, 1.4f), (currentPullTime / (pullCounter - Time.time)) - 1);
				pulled = false;
			}
			else
			{
				if (targetChicken != null)
				{
					this.transform.position = new Vector3(targetChicken.transform.position.x, 1.8f);
					targetChicken.movementSpeed = 0;


					if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
					{
						anim.Play("Pull");
						Destroy(targetChicken.gameObject, 1);

						if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
						{
							if (!pulled)
							{
								FindObjectOfType<ChickenSpawner>().numberOfChickens--;
								pulled = true;
							}
						}
					}
				}
				else
				{
					if(FindObjectOfType<ChickenSpawner>().numberOfChickens < 1)
					{
						if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
						{
							this.transform.position = player.transform.position + new Vector3(0, 1.4f);
							player.movementSpeed = 0;
						}
						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
						{
							this.transform.position = player.transform.position + new Vector3(0, 1.4f);
							anim.Play("Pull");
							deadChickenSprite.color = Color.white;
							Destroy(player.gameObject, 1);
							Timer = Time.time + 5;
						}
						
					}
					reTarget();
				}
			}
		}
		else
		{
			this.transform.position = player.transform.position + new Vector3(0, 1.4f);
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
			{
				anim.Play("Pull");
				deadChickenSprite.color = Color.white;
				Destroy(player.gameObject, 1);
				Timer = Time.time + 5;
			}
		}
	}





	public void reTarget()
	{

		if (FindObjectOfType<ChickenSpawner>().numberOfChickens > 0)
		{
			targetChicken = FindObjectsOfType<ChickenAI>()[Random.Range(0, FindObjectOfType<ChickenSpawner>().numberOfChickens)];
			currentPullTime = Random.Range(pullTimeMin, pullTimeMax);
			pullCounter = Time.time + currentPullTime;
		}
	}
}


/*

	
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
		{
			Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
			if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
			{
				this.transform.position = new Vector3(Mathf.Sin(Time.time * Speed) * 3.5f, 1.8f, 0);
			}
		}
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pull"))
		{
			this.transform.position = new Vector3(Mathf.Sin(Time.time * Speed) * 3.5f, 1.8f, 0);
			if (pullCounter < Time.time)
			{
				anim.Play("Pull");
				chickens = FindObjectsOfType<ChickenAI>();
				if (chickens.Length != 0)
				{
					float[] dist = new float[chickens.Length];

					for (int i = 0; i < chickens.Length; i++)
					{
						dist[i] = (this.transform.position - chickens[i].transform.position).sqrMagnitude;
					}
					float min = Mathf.Min(dist);
					for (int j = 0; j < dist.Length; j++)
					{
						if (dist[j] == min)
						{
							Debug.Log(chickens[j].name);
							chickens[j].movementSpeed = 0;
							Destroy(chickens[j].gameObject, 1);
						}
					}

					pullCounter = Time.time + Random.Range(pullTimeMin, pullTimeMax);
				}
				else
				{

				}
			}
		}



 */
