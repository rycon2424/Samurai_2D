using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	public float speed;

	void Update ()
	{
		if (Input.GetKey(KeyCode.W))
		{
			speed = Mathf.Pow(speed, 0.99f);
		} 
		else 
		{
			if (speed > 2) 
			{
				speed = 2;
			}
			speed = Mathf.Pow(speed, 1.01f);
		}

	}

}