using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public float speed;
	public Animator anim;
	public static bool death = false;
	public BoxCollider collider;
	public BoxCollider deathCollider;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		anim.SetInteger ("State", 1);
		deathCollider.enabled = false;

		Physics.IgnoreLayerCollision(7, 8);
	}

	void Update () 
	{
		if (death == true)
		{
			speed = 0;
			anim.SetInteger ("State", 3);
		}

		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "ground")
		{
			transform.Rotate (0, 180, 0);
		}

		if (col.gameObject.tag == "Player") 
		{
			anim.SetInteger ("State", 2);
			deathCollider.enabled = true;
		}

		if (col.gameObject.tag == "Sword")
		{
			anim.SetInteger ("State", 3);
			collider.enabled = false;
			deathCollider.enabled = false;
			speed = 0;
		}

		/*if (col.gameObject.CompareTag ("Zombie"))
		{
			Physics.IgnoreCollision(col.GetComponent<Collider>(), collider);
		}*/
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			deathCollider.enabled = false;
			anim.SetInteger ("State", 1);
		}
	}
}
