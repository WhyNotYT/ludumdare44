/* Copyright (c) Why?Not!YT - 2019 
 * 
 * This is the source code for the game for Ludum Dare 44
 */

using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed = 10;
	public bool Grounded;
	public float JumpForce;
	public LayerMask layer;
	public ParticleSystem blood;
	public bool dead;
	


	private Rigidbody2D rb;
	private BoxCollider2D box;
	private CircleCollider2D circle;
	private Vector2 finalVel;
	private SpriteRenderer sRenderer;
	private Animator anim;
	private float Hori;
	private PlayerStats stats;
	private float flashCounter;
	private void Start()
	{
		rb = this.GetComponent<Rigidbody2D>();
		sRenderer = this.GetComponent<SpriteRenderer>();
		anim = this.GetComponent<Animator>();
		anim.Play("Walk");
		stats = FindObjectOfType<PlayerStats>();
	}



	private void Update()
	{

		if(flashCounter > Time.time)
		{
			sRenderer.color = Color.white * Mathf.Abs(Mathf.Sin(Time.time * 50));
		}
		else
		{
			sRenderer.color = Color.white;
		}

		if (!dead)
		{
			Hori = Input.GetAxis("Horizontal");
			if (Hori != 0)
			{
				if (Hori < 0)
				{
					sRenderer.flipX = false;
				}
				else
				{
					sRenderer.flipX = true;
				}
				if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Peck"))
				{
					finalVel.x = Hori * movementSpeed * Mathf.Clamp(Mathf.Abs(Mathf.Sin(Time.time * 1024)), 0.7f, 1f);
					//Debug.Log(finalVel.x);
				}
				else
				{
					finalVel.x = 0;
				}
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Sit"))
				{
					anim.Play("Walk");
				}
			}
			else
			{
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
				{
					anim.Play("Idle");
				}
				finalVel.x = 0;
			}


			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Peck"))
				{
					anim.Play("Peck");
					RaycastHit2D hit;
					if (!sRenderer.flipX)
					{
						hit = Physics2D.Raycast(this.transform.position, Vector3.left, 0.5f, layer);
					}
					else
					{
						hit = Physics2D.Raycast(this.transform.position, Vector3.right, 0.5f, layer);
					}
					if (hit)
					{
						if (hit.collider.gameObject.name == "Drinking Thing")
						{
							stats.Thirst += stats.thirstPerPeck;
						}
						else if (hit.collider.gameObject.name == "Eating Thing")
						{
							stats.Hunger += stats.hungerPerPeck;
						}
						stats.Validate();
						stats.RateCounter = Time.time + 0.25f;
					}
				}
			}

			if (Input.GetAxis("Vertical") < 0)
			{
				if (Hori == 0)
				{
					anim.Play("Sit");
					finalVel.x = 0;
					stats.Rate = 0.5f;
					stats.healthPerTick = 10;
				}
			}
			else
			{
				stats.Rate = 0.25f;
				stats.healthPerTick = 2;
			}


			rb.velocity = finalVel;
		}
	}

	public void Die()
	{
		dead = true;
		anim.Play("Dead");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		//Debug.Log(collision.collider.name);
		if (collision.collider.CompareTag("Ground"))
		{
			Grounded = true;
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{

		if (collision.collider.CompareTag("Ground"))
		{
			//Debug.Log(collision.collider.name);
			Grounded = false;
		}
	}



	public void takeDamage(Vector3 from)
	{
		stats.Health -= stats.healthPerHit;
		flashCounter = Time.time + 0.5f;
		this.GetComponent<AudioSource>().Play();
	}
}
