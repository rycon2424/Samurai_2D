using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

	public float speed;
	public bool fire = false;

	// Use this for initialization
	void Start () 
	{
		Destroy (this.gameObject, 5);
		StartCoroutine(Flames());
	}

	// Update is called once per frame
	void Update () 
	{
		if (fire)
		{
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			Player.death = true;
		}
	}

	IEnumerator Flames()
	{
		yield return new WaitForSeconds (0.5f);
		fire = true;
	}
}
