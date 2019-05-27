using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
	public float Rate = 0.25f;


	public int thirshtDecreaseTick = 2;
	public float thirstPerPeck = 5;


	public int hungerDecreaseTick = 4;
	public float hungerPerPeck = 5;

	public int healthIncreaseTick = 2;
	public float healthPerHit = 2;
	public float healthPerTick = 2; 



	public Image thirstBar;
	public Image hungerBar;
	public Image healthBar;

	public float Thirst = 100;
	public float Hunger = 100;
	public float Health = 100;

	private int tick;
	private PlayerController player;
	public float RateCounter;





	private void Start()
	{
		player = FindObjectOfType<PlayerController>();
	}

	void Update()
    {
		if (!player.dead)
		{
			if (RateCounter < Time.time)
			{
				tick++;

				if (tick % thirshtDecreaseTick == 0)
				{
					Thirst -= 1.5f;
				}
				if (tick % hungerDecreaseTick == 0)
				{
					Hunger -= 2;
				}
				if (tick % healthIncreaseTick == 0)
				{
					if (Health < 100)
					{
						Health += healthPerTick;
					}
				}
				UpdateBars();
				RateCounter = Time.time + Rate;
			}
		}
		else
		{
			Health = -1000;
			Hunger = -1000;
			Thirst = -1000;
			UpdateBars();
		}
		if(Health < 1 || Hunger < 1 || Thirst < 1)
		{
			player.Die();
		}
    }

	public void Validate()
	{

		if (Health > 100) Health = 100;
		if (Hunger > 100) Hunger = 100;
	}
	public void UpdateBars()
	{

		healthBar.fillAmount = Health / 100;
		hungerBar.fillAmount = Hunger / 100;
		thirstBar.fillAmount = Thirst / 100;
	}
}
