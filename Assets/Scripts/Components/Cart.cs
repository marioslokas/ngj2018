using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField]
    internal float CartLength = 1;

    internal Shapes MyShape;

    public void Init(Shapes myShape)
    {
        MyShape = myShape;

        Debug.Log(myShape);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PersonBehavior pb;

        if ((pb = collision.collider.GetComponent<PersonBehavior>()) != null)
        {
            if (MyShape == Shapes.All || MyShape == pb.MyShape) return;

            //ToDo Panic or run out?
            Destroy(pb.gameObject);
        }
    }
}