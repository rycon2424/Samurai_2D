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
	public bool usingSword = true;
	bool onlyOnce = true;
	bool isRolling = false;

	public Transform canvasOfDeath;

	public float forceConst;
	public Rigidbody selfRigidbody;

	public static bool jumping = false;
	private bool lookingLeft = false;

	public bool imDead = false;

	public Collider sword;
	public GameObject swordHand;
	public GameObject bowHand;
	public GameObject swordBack;
	bool swapCooldown;
	public bool arrowDraw;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		mainCollider = this.transform.gameObject.GetComponent<CapsuleCollider>();
		swordBack.SetActive (false);
		bowHand.SetActive (false);
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

		#endregion

		#region swap

		if (Input.GetKeyDown(KeyCode.E) && !swapCooldown)
		{
			anim.SetInteger ("State", 5);
			usingSword = !usingSword;
			if (swordBack.activeSelf)
			{
				swordBack.SetActive(false);
			}else
			{
				swordBack.SetActive(true);
			}
		}

		if (swordBack.activeSelf && !usingSword)
		{
			swordHand.SetActive (false);
			bowHand.SetActive (true);
		}
		if (!swordBack.activeSelf && usingSword)
		{
			swordHand.SetActive (true);
			bowHand.SetActive (false);
		}

		#endregion

		#region Sword

		if (swordBack.activeSelf)
		{
			anim.SetBool ("Sword", false);
		}else
		{
			anim.SetBool ("Sword", true);
		}

		if (usingSword) {
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

			if (Input.GetMouseButton(0))
			{
				anim.SetInteger ("State", 4);
				sword.enabled = true;
			}
			else 
			{
				sword.enabled = false;
			}

			if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)
				|| Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.LeftShift)
					|| (Input.GetKeyUp(KeyCode.Space)) || (Input.GetMouseButtonUp(0)) 
					|| (Input.GetKeyUp(KeyCode.E))))
			{
				moving = false;
				anim.SetInteger ("State", 0);

			}
		}

		#endregion

		#region bow

		if (!usingSword)
		{
			if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.A)))
			{
				moving = true;
				if (Input.GetKey(KeyCode.LeftShift))
				{	
					anim.SetInteger ("State", 2);
					anim.SetBool ("Drawn", false);
				}
				else 
				{
					anim.SetInteger ("State", 1);
				}
			}
			else 
			{
				anim.SetInteger ("State", 0);
				moving = false;
			}

			if (Input.GetMouseButton(0))
			{
				if (!moving)
				{
					anim.SetInteger ("State", 3);
				}
				else 
				{
					anim.SetInteger ("State", 6);
				}
				arrowDraw = true;
				anim.SetBool ("Drawn", true);
			}
			else 
			{
				arrowDraw = false;
				anim.SetBool ("Drawn", false);
			}

			if (Input.GetKeyUp(KeyCode.D) && Input.GetKeyUp(KeyCode.A)
				|| Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.LeftShift)
					|| (Input.GetKeyUp(KeyCode.Space)) || (Input.GetMouseButtonUp(0)) 
					|| (Input.GetKeyUp(KeyCode.E))))
			{
				anim.SetInteger ("State", 0);
			}
		}

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)
			|| Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.LeftShift)
				|| (Input.GetKey(KeyCode.Space)) || (Input.GetMouseButton(0)) 
				|| (Input.GetKey(KeyCode.E))))
		{
			swapCooldown = true;
		}
		else
		{
			swapCooldown = false;
		}

	}

		#endregion


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
