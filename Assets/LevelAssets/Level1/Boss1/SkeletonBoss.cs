using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonBoss : MonoBehaviour {

	public bool alive = true;

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
	public float speed = 1;

	[Header("Phase 1")]
	public Transform fireBall;
	public Transform fireBallSpawn1;
	public Transform fireBallSpawn2;
	public float attackRange = 2.5f;
	bool hurt = false;
	bool stunned = false;
	bool attacking = false;
	bool cast = false;
	bool canCastRanged = false;

	[Header("Phase 2")]
	public GameObject secondSword;
	public GameObject particle1;
	public GameObject explosion;
	bool attackCooldown = false;
	bool canBeHitSecondPhase = true;
	bool transformAnimation = false;
	bool doOnce = true;
	public bool running = false;
	public GameObject exMark;


	void Start () 
	{
		secondSword.SetActive (false);
		exMark.SetActive (false);
		particle1.SetActive (false);
		explosion.SetActive (false);
		hpBarVisual.SetActive (false);
		sword.SetActive (false);
		pillar.SetActive (false);
		anim.SetInteger ("State", 1);
		hpBoss = 30;
	}

	void Update () 
	{
		hpBar.value = hpBoss;
		if (hpBoss < 1)
		{
			secondSword.SetActive (false);
			particle1.SetActive (false);
			explosion.SetActive (false);
			hpBarVisual.SetActive (false);
			sword.SetActive (false);
			pillar.SetActive (false);
			alive = false;
			anim.SetInteger ("State", 9);
		}
		
		if (bossFightStarted && (whatPhase == bossFightState.first || whatPhase == bossFightState.second) && alive) 
		{
			myVector = gameObject.transform.position;
			thePlayerV3 = player.transform.position;

			if (!attacking && !stunned && !transformAnimation && !running)
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

			if (Vector3.Distance(thePlayerV3, transform.position) < attackRange && !stunned && !transformAnimation && !attackCooldown) 
			{
				canCastRanged = true;
				attacking = true;
				anim.SetInteger ("State", 3);
				sword.SetActive (true);
				if (whatPhase == bossFightState.second)
				{
					StartCoroutine(SecondSwing());
				}
			} else 
			{
				if (whatPhase == bossFightState.first) {
					sword.SetActive (false);
					attacking = false;
				}
			}

			if (Vector3.Distance(thePlayerV3, transform.position) > 7.5f && !stunned && canCastRanged && !transformAnimation) 
			{
				canCastRanged = false;
				StartCoroutine(RangedAttack());
			} 

			if (Vector3.Distance(thePlayerV3, transform.position) > 6f && !stunned && !transformAnimation && whatPhase == bossFightState.second) 
			{
				running = true;
				transform.Translate (Vector3.forward * speed * Time.deltaTime);
				anim.SetInteger ("State", 8);
				speed = 4;
			} else {
				running = false;
				speed = 1;
			}
		}

		if (hpBoss < 16 && alive) 
		{
			if (doOnce) 
			{
				transformAnimation = true;
				StartCoroutine(PhaseTwo());
				doOnce = false;
			}
			secondSword.SetActive (true);
			particle1.SetActive (true);
			explosion.SetActive (true);
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

		if (bossFightStarted && alive)
		{
			if (col.gameObject.CompareTag("Sword") && !hurt && !transformAnimation && whatPhase == bossFightState.first)
			{
				hurt = true;
				stunned = true;
				StartCoroutine(Hurt());
			}

			if (col.gameObject.CompareTag("Sword") && !hurt && !transformAnimation && whatPhase == bossFightState.second && canBeHitSecondPhase)
			{
				canBeHitSecondPhase = false;
				StartCoroutine(HurtSecondPhase());
			}
		}
	}

	IEnumerator HurtSecondPhase ()
	{
		hpBoss = hpBoss - 2;
		yield return new WaitForSeconds (1.7f);
		canBeHitSecondPhase = true;
	}

	IEnumerator Hurt ()
	{
		sword.SetActive (false);
		hpBoss = hpBoss - 2;
		anim.SetInteger ("State", 4);
		yield return new WaitForSeconds (1.7f);
		stunned = false;
		yield return new WaitForSeconds (1f);
		hurt = false;
	}

	IEnumerator RangedAttack ()
	{
		int chance;
		chance = Random.Range (1, 3);
		if (chance == 1)
		{
			Instantiate (fireBall, fireBallSpawn1.position, fireBallSpawn1.rotation);
		} else {
			Instantiate (fireBall, fireBallSpawn2.position, fireBallSpawn2.rotation);
		}
		yield return new WaitForSeconds (3f);
		canCastRanged = true;
	}

	IEnumerator PhaseTwo()
	{
		anim.SetInteger ("State", 5);
		yield return new WaitForSeconds (2.5f);
		whatPhase = bossFightState.second;
		transformAnimation = false;
	}

	IEnumerator SecondSwing()
	{
		attacking = true;
		attackCooldown = true;
		sword.SetActive (true);
		yield return new WaitForSeconds (1f);
		int chance;
		chance = Random.Range (1, 8);
		if (chance == 1)
		{
			exMark.SetActive (true);
		}
		if (chance == 5)
		{
			exMark.SetActive (true);
		}
		anim.SetInteger ("State", 6);
		yield return new WaitForSeconds (1.5f);
		anim.SetInteger ("State", 7);
		sword.SetActive (false);
		attacking = false;
		if (chance == 1)
		{
			Instantiate (fireBall, fireBallSpawn1.position, fireBallSpawn1.rotation);
			exMark.SetActive (false);
		}
		if (chance == 5)
		{
			Instantiate (fireBall, fireBallSpawn2.position, fireBallSpawn2.rotation);
			exMark.SetActive (false);
		}
		yield return new WaitForSeconds (4f);
		attackCooldown = false;
	}
}