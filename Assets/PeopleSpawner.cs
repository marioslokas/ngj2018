using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public float spawningInterval = 10f;

    public bool canSpawn = true;

    public PersonBehavior[] personPrefabs;

    private GameObject spawningPoint;

    private float currentElapsedTime;

    // Use this for initialization
    private void Start()
    {
        currentElapsedTime = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        currentElapsedTime += Time.deltaTime;

        if (currentElapsedTime > spawningInterval && canSpawn)
        {
            GameObject spawnPoint = PickSpawnPoint();
            PersonBehavior personPrefab = null;

            var shape = (Shapes)Random.Range(2, 5);

            for (int i = 0; i < personPrefabs.Length; i++)
            {
                if (personPrefabs[i].MyShape == shape)
                {
                    personPrefab = personPrefabs[i];
                }
            }

            var newSphere = Instantiate(personPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            currentElapsedTime = 0f;
        }
    }

    private GameObject PickSpawnPoint()
    {
        int spawnPointsNumber = spawnPoints.Length;
        int randomPoint = UnityEngine.Random.Range(0, spawnPointsNumber);

        return spawnPoints[randomPoint];
    }
}