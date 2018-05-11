using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public GameObject normal;
	public GameObject boss;

	void Start () 
	{
		normal.SetActive (true);
		boss.SetActive (false);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			normal.SetActive (false);
			boss.SetActive (true);
		}
	}
}