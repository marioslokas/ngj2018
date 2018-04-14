using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehavior : MonoBehaviour
{
    private bool isOnPlatform;
    private bool isOnCrane;
    private bool isOnTrain;

    public Shapes MyShape { get; private set; }

    private void Awake()
    {
        MyShape = (Shapes)Random.Range(1, 4);

        Debug.Log(MyShape);
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag.Equals("Platform"))
        {
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.gameObject.tag.Equals("Platform"))
        {
            isOnPlatform = false;
        }
    }
}