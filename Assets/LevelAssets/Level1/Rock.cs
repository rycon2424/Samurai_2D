using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public GameObject rock;
	public BoxCollider trigger;
	public GameObject block;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			rock.AddComponent<Rigidbody>();
			trigger.enabled = false;
			StartCoroutine(Solid());
		}
	}

	IEnumerator Solid()
	{
		yield return new WaitForSeconds (5);
		rock.GetComponent<Rigidbody> ().mass = 100;
		block.SetActive (true);
	}
}
