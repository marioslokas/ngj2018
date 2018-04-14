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
    }
}