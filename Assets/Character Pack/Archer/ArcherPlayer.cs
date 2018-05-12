using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcherPlayer : MonoBehaviour {

	public static bool death;

	public float speed;
	bool moving = false;
	public Transform playerCamera;
	public CapsuleCollider mainCollider;
	public Animator anim;
	bool onlyOnce = true;
	bool isRolling = false;

	float normalHitboxX = 98;
	float normalHitboxY = 238;
	float normalHitboxZ = 90;
	float rollingHitboxX = 0;
	float rollingHitboxY = 0;
	float rollingHitboxZ = 0;

	public Transform canvasOfDeath;

	public float forceConst;
	public Rigidbody selfRigidbody;

	public static bool jumping = false;
	private bool lookingLeft = false;

	public bool imDead = false;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		mainCollider = this.transform.gameObject.GetComponent<CapsuleCollider>();
	}

	void Update ()
	{
		if (death == true)
		{
			imDead = true;
			speed = 0;
			StartCoroutine(Restart());
		}

		#region movement

		if (Input.GetKey(KeyCode.LeftShift) && !imDead)
		{
			speed = 4;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift) && !imDead)
		{
			speed = 2;
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			lookingLeft = true;
			transform.rotation = Quaternion.Euler (-0, -90, 0);
		}

		if (Input.GetKey(KeyCode.D))
		{
			lookingLeft = false;
			transform.Translate (Vector3.back * -speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			transform.rotation = Quaternion.Euler (-0, 90, 0);
		}

		if (Input.GetKey(KeyCode.W) && !jumping)
		{
			selfRigidbody.AddForce (0, forceConst, 0, ForceMode.Impulse);
			StartCoroutine(Jump());
		}

		if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
		{
			isRolling = true;
			mainCollider.height = 20;
			anim.SetInteger ("State", 3);
			StartCoroutine(Rol());
			if (lookingLeft)
			{
				selfRigidbody.AddForce (-4, 2, 0, ForceMode.Impulse);
			}
			if (!lookingLeft)
			{
				selfRigidbody.AddForce (4, 2, 0, ForceMode.Impulse);
			}
		}

		#endregion

		#region animation

		if (Input.GetMouseButton(0))
		{
			anim.SetInteger ("State", 4);
		}

		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)
			|| Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.LeftShift)
				|| (Input.GetKeyUp(KeyCode.Space)) || (Input.GetMouseButtonUp(0))))
		{
			moving = false;
			anim.SetInteger ("State", 0);
		}

		if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.A)))
		{
			moving = true;
			if (Input.GetKey(KeyCode.LeftShift))
			{	
				if (Input.GetMouseButton(0))
				{
					anim.SetInteger ("State", 4);
				}
				else
				{
				anim.SetInteger ("State", 2);
				}
			}
			else
			{
				anim.SetInteger ("State", 1);
			}
		}

		#endregion
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Death"))
		{
			death = true;
			imDead = true;
			speed = 0;
			StartCoroutine(Restart());
		}
	}

	IEnumerator Jump()
	{
		yield return new WaitForSeconds (0.2f);
		jumping = true;
	}

	IEnumerator Rol()
	{
		yield return new WaitForSeconds (0.8f);
		mainCollider.height = 220.7062f;
		isRolling = false;
	}


	IEnumerator Restart()
	{
		if (onlyOnce == true)
		{
			playerCamera.transform.parent = null;
			Instantiate (canvasOfDeath, transform.position, transform.rotation);
			onlyOnce = false;
			mainCollider.enabled = false;
		}
		yield return new WaitForSeconds (5);
		death = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
