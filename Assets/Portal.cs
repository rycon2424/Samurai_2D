using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	public string levelName;
	public Vector3 spawnPosition;

	void Start()
	{
		transform.position = spawnPosition;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
			SceneManager.LoadScene (levelName);
	}
}
