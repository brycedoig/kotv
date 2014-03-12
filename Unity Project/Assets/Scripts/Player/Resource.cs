using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour 
{
	protected int resource = 16;
	protected int maxResource = 16;
	protected int minResource = 0;
	protected int regenSpeed = 0; //per second
	
	protected virtual void Start() 
	{
		
	}

	protected virtual void Update() 
	{
		
	}

	protected virtual void DoDelta(int delta)
	{
		resource += delta;
		resource = Mathf.Clamp(resource, minResource, maxResource);
	}
}