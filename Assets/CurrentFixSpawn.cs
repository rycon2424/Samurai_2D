using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentFixSpawn : MonoBehaviour {

	void Update () 
	{
		if (Input.GetKey(KeyCode.J))
		{
			transform.position = new Vector3 (40,-11,0);
		}
		if (Input.GetKey(KeyCode.K))
		{
			transform.position = new Vector3 (112.6f,-12,0);
		}
	}
}
