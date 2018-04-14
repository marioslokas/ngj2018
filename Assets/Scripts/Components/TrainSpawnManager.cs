using System;
using UnityEngine;
using UnityEngine.UI;

public class TrainSpawnManager : MonoBehaviourSingleton<TrainSpawnManager>, ISingleton
{
    [SerializeField]
    private float timeBetweenTrains;

    [SerializeField]
    private float timeBetweenWavesOfPeople;

    [SerializeField]
    private Cart cartPrefab;

    [SerializeField]
    private Transform trainParentPrefab;

    [SerializeField]
    private Transform eventSystemPrefab;

    [SerializeField]
    private UIChildReference canvasForUIPrefab;

    [SerializeField]
    private GameObject[] objectsToSpawnFromInit;

    //ToDo setup reference to people prefab
    [SerializeField]
    private GameObject peoplePrefab;

    private Text textTimerRef;
    private int minCartCount = 1;
    private int maxCartCount = 5;
    private int minPlebCount = 15;
    private int maxPlebCount = 18;
    private string basisTimerText;
    private float betweenWavesOfPeopleTimer;

    internal float BetweenTrainsTimer;

    public void Init()
    {
        BetweenTrainsTimer = timeBetweenTrains;
        betweenWavesOfPeopleTimer = timeBetweenWavesOfPeople;

        Instantiate(eventSystemPrefab);
        textTimerRef = Instantiate(canvasForUIPrefab).TimerText;
        basisTimerText = textTimerRef.text;
        SpawnTrain();

        for (int i = 0; i < objectsToSpawnFromInit.Length; i++)
        {
            Instantiate(objectsToSpawnFromInit[i]);
        }
    }

    private void SpawnTrain()
    {
        var trainRef = Instantiate(trainParentPrefab);

        int amountOfCarts = UnityEngine.Random.Range(minCartCount, maxCartCount);

        for (int i = 0; i < amountOfCarts; i++)
        {
            var cartRef = Instantiate(cartPrefab, trainRef);

            cartRef.transform.localPosition = Vector3.zero - new Vector3(cartRef.CartLength * i * 1.2f, 0, 0);
        }
    }

    private void Update()
    {
        BetweenTrainsTimer -= Time.deltaTime;
        betweenWavesOfPeopleTimer -= Time.deltaTime;

        textTimerRef.text = basisTimerText + (int)BetweenTrainsTimer;

        if (timeBetweenWavesOfPeople <= 0)
        {
            betweenWavesOfPeopleTimer = timeBetweenWavesOfPeople;

            //Spawn the wave of people

            int amountOfPeople = UnityEngine.Random.Range(minPlebCount, maxPlebCount);

            for (int i = 0; i < amountOfPeople; i++)
            {
                var pleb = Instantiate(peoplePrefab);

                //Set offset on pleb pos
            }
        }
    }

    private void LateUpdate()
    {
        if (BetweenTrainsTimer <= 0)
        {
            SpawnTrain();
            BetweenTrainsTimer = timeBetweenTrains;
        }
    }
}