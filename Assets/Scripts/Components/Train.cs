using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    private Rigidbody body;
    private bool hasBeenStoopped;
    private bool stopped = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (TrainSpawnManager.Instance.BetweenTrainsTimer <= 0)
        {
            stopped = false;
        }

        if (!stopped)
        {
            //ToDo movement
            body.velocity = new Vector3(1, 0, 0) * Time.deltaTime * speed;
        }
        else
        {
            body.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenStoopped && other.gameObject.tag == "StopSign")
        {
            stopped = true;
            hasBeenStoopped = true;
        }
        else if (hasBeenStoopped && other.gameObject.tag == "DestroyTrain")
        {
            Destroy(gameObject);
        }
    }
}