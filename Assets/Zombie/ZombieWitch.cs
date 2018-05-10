using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWitch : MonoBehaviour {

	public Transform player;
	public Transform aim;
	public Transform bullet;
	public Transform lightingRain;
	public GameObject RedMark;

	public Collider triggerCollider;
	public Collider deathCollider;
	public Collider zombieCollider;

	public Vector3 myVector;

	public Animator anim;

	public bool triggered = false;
	bool attackCld = true;

	bool dead = false;

	void Start () 
	{
		RedMark.SetActive (false);
		myVector = gameObject.transform.position;
	}

	void Update () 
	{
		if (!dead) {
			if (player.transform.position.x > myVector.x)
			{
				transform.rotation = Quaternion.Euler (0, 90, 0);
			}

			if (player.transform.position.x < myVector.x)
			{
				transform.rotation = Quaternion.Euler (0, -90, 0);
			}

			if (triggered) 
			{
				aim.transform.LookAt (player.transform.position);
				aim.transform.Rotate (new Vector3 (0, -90, -90), Space.Self);
				if (attackCld)
				{
					int random;
					random = Random.Range (1, 11);
					if (random > 2 || random == 2)
					{
						Instantiate (bullet, aim.transform.position, aim.transform.rotation);
					}
					if (random < 2)
					{
						StartCoroutine(LightingAttack());
					}
					attackCld = false;
					StartCoroutine(Cooldown());
				}
			}
		}
	}

	IEnumerator LightingAttack()
	{
		RedMark.SetActive (true);
		yield return new WaitForSeconds (1f);
		RedMark.SetActive (false);
		Instantiate (lightingRain, gameObject.transform.position, lightingRain.transform.rotation);
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (3f);
		attackCld = true;
	}

	void OnTriggerEnter(Collider col)
	{

		if (col.gameObject.CompareTag("Player")) 
		{
			anim.SetInteger ("State", 1);
			triggerCollider.enabled = false;
			triggered = true;
		}

		if (col.gameObject.tag == "Sword" && triggered)
		{
			dead = true;
			anim.SetInteger ("State", 3);
			zombieCollider.enabled = false;
			deathCollider.enabled = false;
		}

	}
}
