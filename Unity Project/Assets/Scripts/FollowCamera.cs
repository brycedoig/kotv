using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public float baseLerpSpeed = 4f;
	public float velocityLerpSpeed = 4f;
	public float velocityLerpScale = 0.5f;
	public Vector3 baseOffset = Vector3.zero;
	private Vector3 velocityOffset = Vector3.zero;
	public float maxXVelocityOffset = 1f;
	public float maxYVelocityOffset = 2f;
	private GameObject followObject = null;
	// Use this for initialization
	void Start () 
	{
		followObject = GameObject.FindGameObjectWithTag("Player");
		if(!followObject)
			Debug.Log("No followObject found!");
	}

	void GetVelocityOffset()
	{
		Vector2 vel = followObject.rigidbody2D.velocity;
		float maxSpeed = followObject.GetComponent<PlayerController>().maxXSpeed;
		Vector3 targetOffset = new Vector3(vel.x, vel.y, 0f) * velocityLerpScale;

		velocityOffset = Vector3.Lerp(velocityOffset, targetOffset, Time.fixedDeltaTime * velocityLerpSpeed);
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		GetVelocityOffset();
		transform.position = Vector3.Lerp(transform.position, followObject.transform.position + baseOffset + velocityOffset, Time.fixedDeltaTime * baseLerpSpeed);
	}
}
