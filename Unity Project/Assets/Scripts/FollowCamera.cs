using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public Vector3 followOffset = Vector3.zero;
	public float speed = 4f;
	private GameObject followObject = null;
	// Use this for initialization
	void Start () 
	{
		followObject = GameObject.FindGameObjectWithTag("Player");
		if(!followObject)
			Debug.Log("No followObject found!");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position = Vector3.Lerp(transform.position, followObject.transform.position + followOffset, Time.deltaTime * speed);
	}
}
