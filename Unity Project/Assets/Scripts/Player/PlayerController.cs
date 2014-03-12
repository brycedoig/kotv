using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour 
{
	public float speed = 10f;
	public float jumpStrength = 10f;
	public int numJumps = 2;
	public float airControl = 1f;
	public float groundControl = 5f;
	public float yNegation = 0.75f;
	 
	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update ()
	{
		rigidbody2D.velocity += HandleInput();
	}

	Vector2 HandleInput()
	{
		Vector2 velocityChange = Vector2.zero;

		velocityChange += Vector2.right * (Input.GetAxis("Horizontal") * speed);

		if(Input.GetButtonDown("Jump"))
		{
			velocityChange += DoJump();
		}

		return velocityChange;
	}

	Vector2 DoJump()
	{
		Vector2 jumpVec = Vector2.up * jumpStrength;
		jumpVec.y -= rigidbody2D.velocity.y * yNegation;
		return jumpVec;
	}

	bool CanJump()
	{
		return numJumps > 0;
	}


}
