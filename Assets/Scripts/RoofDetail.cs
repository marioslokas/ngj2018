using UnityEngine;

public class RoofDetail : MonoBehaviour
{
    public AudioSource Thump;

    private void OnCollisionEnter(Collision collision)
    {
        //Thump.Play();

        Destroy(gameObject);
    }
}