using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public GameObject player;
	public bool reset = true;

	private Vector3 offset;        

	void Start () 
	{
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () 
	{
		if (ArcherPlayer.death == true) 
		{
			reset = false;
		}
		if (reset) 
		{
			transform.position = player.transform.position + offset;
		}
	}
}