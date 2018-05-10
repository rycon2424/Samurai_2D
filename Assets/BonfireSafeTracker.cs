using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireSafeTracker : MonoBehaviour {

	private static BonfireSafeTracker instance = null;
	public static BonfireSafeTracker Instance {
		get { return instance; }
	}

	void Awake()
	{
		if (instance != null && instance != this)
		{

			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
} 
