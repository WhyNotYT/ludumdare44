using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAI : MonoBehaviour
{
	public State state;
	public float movementSpeed = 1;
	public float idleTimeMin = 3;
	public float idleTimeMax = 5;
	public float eatTimeMin = 1;
	public float eatTimeMax = 3;
	public int AttackChance = 5; // 1 in X




	private PlayerController player;
	private PlayerStats playerStats;
	private Animator anim;
	private float timeCounter;
	private bool Food;
	private float idleTargetX;
	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		
		movementSpeed = Random.Range(0.8f, 1.5f);
		player = FindObjectOfType<PlayerController>();
		playerStats = FindObjectOfType<PlayerStats>();
		anim = this.GetComponent<Animator>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		{
			int a = Random.Range(0, 2);
			if (a == 0)
			{
				state = State.Idle;
			}
			else
			{
				state = State.Eating;
			}
		}
		{

			int a = Random.Range(0, 2);
			if (a == 0)
			{
				Food = false;
				spriteRenderer.flipX = false;
			}
			else
			{
				Food = true;
				spriteRenderer.flipX = true;
			}
		}
	}




	public void Update()
	{
		if(state == State.Idle)
		{
			if(timeCounter < Time.time)
			{
				state = State.Eating;
				timeCounter = Time.time + Random.Range(eatTimeMin, eatTimeMax);
				int a = Random.Range(0, 2);
				if(a == 0)
				{
					Food = false;
					spriteRenderer.flipX = false;
				}
				else
				{
					Food = true;
					spriteRenderer.flipX = true;
				}
			}
			else
			{
				if(Mathf.Round(this.transform.position.x * 10) == Mathf.Round(idleTargetX * 10))
				{
					if ((timeCounter - Time.time) > 1)
					{
						if ((this.transform.position - player.transform.position).sqrMagnitude < 1.5f)
						{
							int a = Random.Range(0, AttackChance + 1);
							//Debug.Log(a);
							if (a == AttackChance)
							{
								if((player.transform.position.x - this.transform.position.x) < 0)
								{
									spriteRenderer.flipX = false;
								}
								else
								{
									spriteRenderer.flipX = true;
								}



								if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Peck"))
								{
									anim.Play("Peck");
								}
								player.takeDamage(this.transform.position);
							}
							else
							{
								if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Peck"))
								{
									anim.Play("Sit");
								}
							}

						}
						else
						{
							anim.Play("Sit");
							
						}
					}
				}
				else
				{
					if(this.transform.position.x < idleTargetX)
					{
						spriteRenderer.flipX = true;

						this.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
						{
							anim.Play("Walk");
						}
					}
					else
					{
						spriteRenderer.flipX = false;

						this.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
						{
							anim.Play("Walk");
						}
					}
				}
			}
		}
		else
		{
			if (timeCounter < Time.time)
			{
				state = State.Idle;
				idleTargetX = Random.Range(-3.5f,3.5f);
				timeCounter = Time.time + Random.Range(idleTimeMin, idleTimeMax);
			}
			else
			{
				if (Food)
				{
					spriteRenderer.flipX = true;
					if (this.transform.position.x < 3.5)
					{
						this.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
						{
							anim.Play("Walk");
						}
					}
					else
					{
						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Peck"))
						{
							anim.Play("Peck");
						}
					}
				}
				else
				{
					spriteRenderer.flipX = false;
					if (this.transform.position.x > -3.5)
					{
						this.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
						{
							anim.Play("Walk");
						}
					}
					else
					{
						if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Peck"))
						{
							anim.Play("Peck");
						}
					}
				}
			}
		}
	}
}

public enum State
{
	Idle,
	Eating,
}