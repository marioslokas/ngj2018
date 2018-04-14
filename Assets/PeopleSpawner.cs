using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

	public GameObject[] spawnPoints;

	public float spawningInterval = 10f;
	public bool canSpawn = true;

	public GameObject personPrefab;

	private GameObject spawningPoint;

	private float currentElapsedTime;

	// Use this for initialization
	void Start () {
		currentElapsedTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		currentElapsedTime += Time.deltaTime;
		if (currentElapsedTime > spawningInterval && canSpawn) {

			GameObject spawnPoint = PickSpawnPoint ();

			GameObject newSphere = Instantiate (personPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

			currentElapsedTime = 0f;
		}
	}

	GameObject PickSpawnPoint()
	{
		int spawnPointsNumber = spawnPoints.Length;
		int randomPoint = UnityEngine.Random.Range (0, spawnPointsNumber);

		return spawnPoints [randomPoint];

	}
}
