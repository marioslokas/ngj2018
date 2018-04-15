using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag.Equals("Person") || other.collider.gameObject.tag.Equals("TrainRoof"))
        {
            GameObject.Destroy(other.collider.gameObject);
        }
    }
}