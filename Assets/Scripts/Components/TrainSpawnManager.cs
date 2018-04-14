using System;
using UnityEngine;
using UnityEngine.UI;

public class TrainSpawnManager : MonoBehaviourSingleton<TrainSpawnManager>, ISingleton
{
    [SerializeField]
    private float timeBetweenTrains;

    [SerializeField]
    private float trainHoldingTime;

    [SerializeField]
	private Transform cartPrefab;

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
    private string timeforDeparture;
    private string timeForArrivals = "Time for arrivals: ";

    internal bool StartedHolding;
    internal float TrainHoldingTimer;
    internal float BetweenTrainsTimer;

    public void Init()
    {
        BetweenTrainsTimer = timeBetweenTrains;
        TrainHoldingTimer = trainHoldingTime;

        Instantiate(eventSystemPrefab);
        textTimerRef = Instantiate(canvasForUIPrefab).TimerText;
        timeforDeparture = textTimerRef.text;
        SpawnTrain(1, startPosDirection);
        SpawnTrain(-1, startNegDirection);

        for (int i = 0; i < objectsToSpawnFromInit.Length; i++)
        {
            Instantiate(objectsToSpawnFromInit[i]);
        }
    }

    private void SpawnTrain(int xDirection, Vector3 startPosition)
    {
        var trainRef = Instantiate(trainParentPrefab);
        trainRef.position = startPosition;
        trainRef.GetComponent<Train>().Init(new Vector3(xDirection, 0, 0));

        var cartRef = Instantiate(cartPrefab, trainRef);

        cartRef.transform.localPosition = new Vector3(xDirection * -3.53f, 0, 0);
//        cartRef.Init((Shapes)UnityEngine.Random.Range(1, 5));
    }

    private void Update()
    {
        BetweenTrainsTimer -= Time.deltaTime;

        if (StartedHolding)
        {
            TrainHoldingTimer -= Time.deltaTime;
            textTimerRef.text = timeforDeparture + (int)TrainHoldingTimer;
        }
        else
        {
            textTimerRef.text = timeForArrivals + (int)BetweenTrainsTimer;
        }
    }

    private void LateUpdate()
    {
        if (BetweenTrainsTimer <= 0)
        {
            SpawnTrain(1, startPosDirection);
            SpawnTrain(-1, startNegDirection);
            BetweenTrainsTimer = timeBetweenTrains;
        }

        if (TrainHoldingTimer <= 0)
        {
            TrainHoldingTimer = trainHoldingTime;
        }
    }
}