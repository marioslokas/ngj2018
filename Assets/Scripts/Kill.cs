using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Person") || other.collider.CompareTag("TrainRoof"))
        {
            Destroy(other.gameObject);
        }
    }
}