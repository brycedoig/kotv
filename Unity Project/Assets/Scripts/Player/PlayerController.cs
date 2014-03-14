using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour 
{
	//public int maxSpeed = 10;
	public float maxXSpeed = 10f;
	public float jumpStrength = 10f;
	public int maxAirJumps = 1;
	private int numJumps = 0;
	public float yNegation = 0.75f;

	public float stopThreshold = 1f;

	public float airMovementForce = 2f;
	public float groundMovementForce = 10f;
	
	public float airStopForce = 2f;
	public float groundStopForce = 10f;

	private bool onGround = false;
	
	// Use this for initialization
	void Start () 
	{
		numJumps = maxAirJumps;

	}

	// Update is called once per frame
	void Update ()
	{
		if(onGround)
			numJumps = maxAirJumps;

		Vector2 newVelocity = rigidbody2D.velocity + GetVelocityChange();

		newVelocity.x = Mathf.Clamp(newVelocity.x, -maxXSpeed, maxXSpeed);

		rigidbody2D.velocity = newVelocity;
	}

	Vector2 GetVelocityChange()
	{
		Vector2 velocityChange = Vector2.zero;
		float hInput = Input.GetAxis("Horizontal");

		if(Mathf.Abs(hInput) < stopThreshold)
			velocityChange -= Vector2.right * GetStopForce() * Time.deltaTime * rigidbody2D.velocity.x;
		else
			velocityChange += Vector2.right * hInput * GetMovementForce() * Time.deltaTime;

		if(Input.GetButtonDown("Jump") && CanJump())
		{
			velocityChange += DoJump();
		}

		return velocityChange;
	}

	Vector2 DoJump()
	{
		if(!OnGround())
			numJumps--;

		Vector2 jumpVec = Vector2.zero;
		jumpVec.y -= rigidbody2D.velocity.y * yNegation;
		jumpVec += (Vector2.up * jumpStrength);
		return jumpVec;
	}

	bool OnGround()
	{
		return onGround;
	}

	float GetStopForce()
	{
		if(OnGround())
			return groundStopForce;
		else
			return airStopForce;
	}

	float GetMovementForce()
	{
		if(OnGround())
			return groundMovementForce;
		else
			return airMovementForce;
	}

	bool CanJump()
	{
		return numJumps > 0;
	}	

	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Ground")
			onGround = true;	
	}

	void OnCollisionExit2D(Collision2D coll) 
	{
		if (coll.gameObject.tag == "Ground")
			onGround = false;		
	}
}
