using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public static float arrowForce;
	public float test;
	public Rigidbody selfRigidbody;

	// Use this for initialization
	void Start () 
	{
		if (arrowForce > 15) 
		{
			if (ArcherPlayer.lookingLeft)
			{
				selfRigidbody.AddForce (-20, 2, 0, ForceMode.Impulse);
			} 
			else
			{
				selfRigidbody.AddForce (20, 2, 0, ForceMode.Impulse);
			}

		} 

		else 
			
		{
			
			if (arrowForce < 8) 
			{
				if (ArcherPlayer.lookingLeft)
				{
					selfRigidbody.AddForce (-2, 1.5f, 0, ForceMode.Impulse);	
				} 
				else
				{
					selfRigidbody.AddForce (2, 1.5f, 0, ForceMode.Impulse);	
				}

			} 
			else
			{
				if (ArcherPlayer.lookingLeft)
				{
					selfRigidbody.AddForce (-arrowForce, 1.5f, 0, ForceMode.Impulse);	
				} 
				else
				{
					selfRigidbody.AddForce (arrowForce, 1.5f, 0, ForceMode.Impulse);
				}
			}

		}

		arrowForce = 0;
		Destroy (this.gameObject, 4);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
