using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChecker : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "ground")
		{
			Player.jumping = false;
			Player.inJumpAnimation = false;
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "ground")
		{
			Player.jumping = false;
			Player.inJumpAnimation = false;
		}
	}
}
