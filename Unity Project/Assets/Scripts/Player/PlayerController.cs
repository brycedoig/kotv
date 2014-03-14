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

	public float wallSlidePower = 1f;
	
	private Transform groundCheckRight;
	private Transform groundCheckLeft;
	
	private Transform rightCheckTop;
	private Transform rightCheckBottom;
	private Transform leftCheckTop;
	private Transform leftCheckBottom;

	private bool onGround = false;
	private bool wallLeft = false;
	private bool wallRight = false;
	private bool shouldJump = false;

	Vector2 newVelocity = Vector2.zero;
	
	// Use this for initialization
	void Start () 
	{
		rigidbody2D.fixedAngle = true;
		numJumps = maxAirJumps;

		groundCheckRight = transform.Find("groundCheckRight");
		groundCheckLeft = transform.Find("groundCheckLeft");

		rightCheckTop = transform.Find("rightCheckTop");
		rightCheckBottom = transform.Find("rightCheckBottom");

		leftCheckTop = transform.Find ("leftCheckTop");
		leftCheckBottom = transform.Find ("leftCheckBottom");
	}

	void FixedUpdate()
	{
		onGround = OnGround();
		wallLeft = WallLeft();
		wallRight = WallRight();
		
		newVelocity = rigidbody2D.velocity + GetVelocityChange();
		newVelocity.x = Mathf.Clamp(newVelocity.x, -maxXSpeed, maxXSpeed);
		rigidbody2D.velocity = newVelocity;
	}

	// Update is called once per frame
	void Update ()
	{
		if(onGround)
			numJumps = maxAirJumps;

		if(Input.GetButtonDown("Jump") && CanJump())
		{
			shouldJump = true;
		}

		if(Input.GetButtonUp("Jump"))
		{

		}		
	}

	Vector2 GetVelocityChange()
	{
		Vector2 velocityChange = Vector2.zero;
		float hInput = Input.GetAxis("Horizontal");

		if(Mathf.Abs(hInput) < stopThreshold)
			velocityChange -= Vector2.right * GetStopForce() * Time.deltaTime * rigidbody2D.velocity.x;
		else
			velocityChange += Vector2.right * hInput * GetMovementForce() * Time.deltaTime;

		if(shouldJump)
		{
			velocityChange += DoJump();
			shouldJump = false;
		}

		float xVel = rigidbody2D.velocity.x + velocityChange.x;
		if(wallRight && xVel > 0 && !wallLeft)
			velocityChange.x = 0;
		else if(wallLeft && xVel < 0 && !wallRight)
			velocityChange.x = 0;

		return velocityChange;
	}

	Vector2 DoJump()
	{
		Vector2 jumpVec = Vector2.zero;
		jumpVec.y -= rigidbody2D.velocity.y * yNegation;

		if(!onGround && !wallLeft && !wallRight)
			numJumps--;

		if(!onGround && wallLeft && !wallRight)
		{			
			jumpVec += (Vector2.up * jumpStrength);
			jumpVec += (Vector2.right * jumpStrength);	
		}
		else if (!onGround && !wallLeft && wallRight)
		{
			jumpVec += (Vector2.up * jumpStrength);
			jumpVec -= (Vector2.right * jumpStrength);			
		}
		else
		{
			jumpVec += (Vector2.up * jumpStrength);			
		}


		return jumpVec;
	}

	bool OnGround()
	{
		return Physics2D.Linecast(groundCheckLeft.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));;
	}

	bool WallLeft()
	{
		return Physics2D.Linecast(leftCheckTop.position, leftCheckBottom.position, 1 << LayerMask.NameToLayer("Ground"));
	}

	bool WallRight()
	{
		return Physics2D.Linecast(rightCheckTop.position, rightCheckBottom.position, 1 << LayerMask.NameToLayer("Ground"));
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
}
