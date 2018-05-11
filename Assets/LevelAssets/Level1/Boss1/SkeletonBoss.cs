using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonBoss : MonoBehaviour {

	[Header("Start Boss")]
	public bool bossFightStarted = false;
	public enum bossFightState {first, second};
	public bossFightState whatPhase;
	public Collider fightTrigger;
	public GameObject pillar;
	public Animator anim;
	public GameObject sword;

	[Header("HEALTH INFO/DISPLAY")]
	public Slider hpBar;
	public int hpBoss;
	public GameObject hpBarVisual;

	[Header("Follow Player")]
	public Transform player;
	public Vector3 thePlayerV3;
	public Vector3 myVector;
	public float speed;

	////////////Phase 1////////////
	bool hurt = false;
	bool stunned = false;
	bool attacking = false;
	bool cast = false;
	bool canCastRanged = false;

	//[Header("Phase 2")]

	void Start () 
	{
		hpBarVisual.SetActive (false);
		sword.SetActive (false);
		pillar.SetActive (false);
		anim.SetInteger ("State", 1);
		hpBoss = 30;
	}

	void Update () 
	{
		hpBar.value = hpBoss;

		if (bossFightStarted && whatPhase == bossFightState.first) 
		{
			myVector = gameObject.transform.position;
			thePlayerV3 = player.transform.position;

			if (!attacking && !stunned)
			{
				anim.SetInteger ("State", 2);
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
			}

			if (player.transform.position.x > myVector.x)
			{
				transform.rotation = Quaternion.Euler (0, 90, 0);
			}

			if (player.transform.position.x < myVector.x)
			{
				transform.rotation = Quaternion.Euler (0, -90, 0);
			}

			///////////Debug.Log(transform.position.x - thePlayerV3.x);

			if (Vector3.Distance(thePlayerV3, transform.position) < 2.5f && !stunned) 
			{
				canCastRanged = true;
				attacking = true;
				anim.SetInteger ("State", 3);
				sword.SetActive (true);
			} else 
			{
				sword.SetActive (false);
				attacking = false;
			}

			if (Vector3.Distance(thePlayerV3, transform.position) > 5f && !stunned && canCastRanged) 
			{
				canCastRanged = false;
				StartCoroutine(RangedAttack());
			} 
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (!bossFightStarted)
		{
			if (col.gameObject.CompareTag("Player")) 
			{
				hpBarVisual.SetActive (true);
				fightTrigger.enabled = false;
				pillar.SetActive (true);
				whatPhase = bossFightState.first;
				bossFightStarted = true;
			}
		}

		if (bossFightStarted)
		{
			if (col.gameObject.CompareTag("Sword") && !hurt)
			{
				Debug.Log ("hit");
				hurt = true;
				stunned = true;
				StartCoroutine(Hurt());
			}
		}
	}

	IEnumerator Hurt ()
	{
		sword.SetActive (false);
		hpBoss = hpBoss - 1;
		anim.SetInteger ("State", 4);
		yield return new WaitForSeconds (1.7f);
		stunned = false;
		yield return new WaitForSeconds (2.3f);
		hurt = false;
	}

	IEnumerator RangedAttack ()
	{
		yield return new WaitForSeconds (2f);
		Debug.Log ("boom");
		canCastRanged = true;
	}
}