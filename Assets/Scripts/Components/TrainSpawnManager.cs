using System;
using UnityEngine;
using UnityEngine.UI;

public class TrainSpawnManager : MonoBehaviourSingleton<TrainSpawnManager>, ISingleton
{
    [SerializeField]
    private float timeBetweenTrains;

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

    private Text textTimerRef;
    private int minCartCount = 1;
    private int maxCartCount = 5;
    private string basisTimerText;

    internal float BetweenTrainsTimer;

    public void Init()
    {
        BetweenTrainsTimer = timeBetweenTrains;

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

        textTimerRef.text = basisTimerText + (int)BetweenTrainsTimer;
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