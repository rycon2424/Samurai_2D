using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireScript : MonoBehaviour {

	bool bonfireLit = false;
	public Transform spawnLocation;
	public Transform litBonfire;
	public Transform litCanvas;

	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Player" && !bonfireLit && Input.GetKey(KeyCode.E))
		{
			Instantiate (litBonfire, spawnLocation.position, spawnLocation.rotation);
			Instantiate (litCanvas, transform.position, transform.rotation);
			bonfireLit = true;
			Destroy (this.gameObject);
		}
	}
}
