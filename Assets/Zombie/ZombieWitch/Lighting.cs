using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () 
	{
		Destroy (this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.up * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			Player.death = true;
		}
	}
}
