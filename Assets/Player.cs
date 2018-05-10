using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public static bool death;

	public float speed;
	public Transform skeleton;
	public Transform playerMovement;
	public Transform playerCamera;
	public Animation anim;
	public static bool inJumpAnimation = false;
	private bool inAttackAnimation = false;
	public BoxCollider sword;
	public Transform canvasOfDeath;
	public bool imDead = false;
	bool onlyOnce = true;

	public float forceConst;
	public Rigidbody selfRigidbody;
	public CapsuleCollider mainCollider;

	public static bool jumping = false;
	private bool moving = false;
	private bool lookingLeft = false;
	bool once = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (death == true && once)
		{
			imDead = true;
			speed = 0;
			StartCoroutine(Restart());
			once = false;
		}

		#region Movement

		if (Input.GetKey(KeyCode.LeftShift) && imDead == false)
		{
			speed = 4;
		}

		if (Input.GetKeyUp(KeyCode.LeftShift) && imDead == false)
		{
			speed = 2;
		}

		if (Input.GetKey(KeyCode.D))
		{
			lookingLeft = false;
			playerMovement.transform.Translate (Vector3.right * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			skeleton.transform.rotation = Quaternion.Euler (-180, 180, 180);
		}

		if (Input.GetKey(KeyCode.A))
		{
			lookingLeft = true;
			playerMovement.transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			skeleton.transform.rotation = Quaternion.Euler (-180, 0, 180);
		}

		if (Input.GetKey(KeyCode.W) && jumping == false)
		{
			anim.Play("Jump");
			inJumpAnimation = true;
			selfRigidbody.AddForce (0, forceConst, 0, ForceMode.Impulse);
			StartCoroutine(Jump());
		}

		#endregion

		#region Animation

		if (Input.GetKey(KeyCode.D) && inJumpAnimation == false && inAttackAnimation == false)
		{
			moving = true;
			if (Input.GetKey(KeyCode.LeftShift) && inJumpAnimation == false && inAttackAnimation == false)
			{
				anim.Play("Run");
			}
			else 
			{
				anim.Play("Walk");
			}
		}

		if (Input.GetKey(KeyCode.Space) && inJumpAnimation == false && inAttackAnimation == false && moving == false)
		{
			inAttackAnimation = true;
			sword.enabled = true;
			if (lookingLeft == true)
			{
				playerMovement.transform.Translate (Vector3.right * -0.3f * Time.deltaTime);
			}

			if (lookingLeft == false)
			{
				playerMovement.transform.Translate (Vector3.right * 0.3f * Time.deltaTime);
			}
			anim.Play("Attack");
		}
		else 
		{
			sword.enabled = false;
			inAttackAnimation = false;
		}

		if (Input.GetKey(KeyCode.A) && inJumpAnimation == false && inAttackAnimation == false)
		{
			moving = true;
			if (Input.GetKey(KeyCode.LeftShift) && inJumpAnimation == false && inAttackAnimation == false)
			{
				anim.Play("Run");
			}
			else 
			{
				anim.Play("Walk");
			}
		}

		if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)
			|| Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.LeftShift)
			|| (Input.GetKeyUp(KeyCode.Space))))
		{
			moving = false;
			anim.Play("idle");
		}

		#endregion

	}

	IEnumerator Jump()
	{
		yield return new WaitForSeconds (0.6f);
		jumping = true;
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Death"))
		{
			imDead = true;
			speed = 0;
			StartCoroutine(Restart());
		}
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
