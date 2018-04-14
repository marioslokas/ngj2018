using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehavior : MonoBehaviour {



	bool isOnPlatform;
	bool isOnCrane;
	bool isOnTrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag.Equals("Platform")) {
			isOnPlatform = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.collider.gameObject.tag.Equals("Platform")) {
			isOnPlatform = false;
		}
	}

}
