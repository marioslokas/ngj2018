using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject[] spawnPoints;

    public float spawningInterval = 10f;

    public bool canSpawn = true;

    public PersonBehavior personPrefab;

    private GameObject spawningPoint;

    private float currentElapsedTime;

    private void Start()
    {
        currentElapsedTime = 0f;
    }

    private void Update()
    {
        currentElapsedTime += Time.deltaTime;

        if (currentElapsedTime > spawningInterval && canSpawn)
        {
            GameObject spawnPoint = PickSpawnPoint();

            PersonBehavior newSphere = Instantiate(personPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            newSphere.MyShape = (Shapes)Random.Range(2, 5);
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