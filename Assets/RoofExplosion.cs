using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnDestroy()
    {
        var exp = Instantiate(explosionPrefab);
        exp.transform.position = transform.position;
    }
}