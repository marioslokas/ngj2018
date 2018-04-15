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
        PersonBehavior person;

        if ((person = GetComponent<PersonBehavior>()) != null && person.isOnTrain)
        {
            TrainSpawnManager.Instance.Score++;
            return;
        }

        if (!m_IsQuitting)
        {
            CameraEffects.RandomShake(6, 0.06f, 0.15f);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}