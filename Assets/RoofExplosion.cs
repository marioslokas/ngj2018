﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    private bool m_IsQuitting = false;

    private void OnApplicationQuit()
    {
        m_IsQuitting = true;
    }

    private void OnDestroy()
    {
        if (!m_IsQuitting)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}