using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	protected int maxHealth = 100;
	protected int health = 100;
	protected int minHealth = 0;
	protected bool showDamageNumbers = false;
	protected bool showHealingNumbers = false;

	// Use this for initialization
	protected virtual void Start() 
	{
	}
	
	// Update is called once per frame
	protected virtual void Update() 
	{
	
	}

	//Change health amounts
	protected virtual void DoDelta(int delta)
	{
		health += delta;
		health = Mathf.Clamp(health, minHealth, maxHealth);

		if(delta < 0)
			OnDamage(delta);
		else if (delta > 0)
			OnHeal(delta);

		if (health == minHealth)
			OnDeath();

	}

	//Called when the entitiy's health <= 0
	protected virtual void OnDeath()
	{

	}

	//Called on DoDelta when delta < 0
	protected virtual void OnDamage(int num)
	{

	}

	//Called on DoDelta when delta > 0
	protected virtual void OnHeal(int num)
	{

	}

	//Returns current health percent
	protected virtual float GetPercent()
	{
		return health/maxHealth;
	}

	//returns current health
	protected virtual int GetHealth()
	{
		return health;
	}

	//returns current max health
	protected virtual int GetMaxHealth()
	{
		return maxHealth;
	}

	//returns curren min health
	protected virtual int GetMinHealth()
	{
		return minHealth;
	}

}
