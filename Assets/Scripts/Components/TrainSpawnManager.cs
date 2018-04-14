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

    [SerializeField]
    private Vector3 startPosDirection;

    [SerializeField]
    private Vector3 startNegDirection;

    private Text textTimerRef;
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
        SpawnTrain(1, startPosDirection);
        SpawnTrain(-1, startNegDirection);

        for (int i = 0; i < objectsToSpawnFromInit.Length; i++)
        {
            Instantiate(objectsToSpawnFromInit[i]);
        }
    }

    private void SpawnTrain(int xDirection, Vector3 startPosition)
    {
        Shapes oldShape = Shapes.All;

        var trainRef = Instantiate(trainParentPrefab);
        trainRef.position = startPosition;
        trainRef.GetComponent<Train>().Init(new Vector3(xDirection, 0, 0));

        for (int i = 0; i < 3; i++)
        {
            var cartRef = Instantiate(cartPrefab, trainRef);

            cartRef.transform.localPosition = Vector3.zero - new Vector3(cartRef.CartLength * i * 1.2f, 0, 0);
            var currentShape = (Shapes)UnityEngine.Random.Range(1, 5);

            if (oldShape == Shapes.All || oldShape != currentShape)
            {
                cartRef.Init(currentShape);
            }
            else
            {
                cartRef.Init(Shapes.All);
            }

            oldShape = currentShape;
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
            SpawnTrain(1, startPosDirection);
            SpawnTrain(-1, startNegDirection);
            BetweenTrainsTimer = timeBetweenTrains;
        }
    }
}