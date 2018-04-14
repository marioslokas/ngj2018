using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.tag.Equals("Person")) {
			GameObject.Destroy (other.collider.gameObject);
		}
	}
}
