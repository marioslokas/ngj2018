using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    private Rigidbody body;
    private Vector3 direction;
    private bool hasBeenStoopped;
    private bool stopped = false;

    public void Init(Vector3 direction)
    {
        this.direction = direction;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (TrainSpawnManager.Instance.TrainHoldingTimer <= 0)
        {
            stopped = false;
            TrainSpawnManager.Instance.StartedHolding = false;
        }

        if (!stopped)
        {
            //ToDo movement
            body.velocity = direction * Time.deltaTime * speed;
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
            TrainSpawnManager.Instance.StartedHolding = true;
        }
        else if (hasBeenStoopped && other.gameObject.tag == "DestroyTrain")
        {
            Destroy(gameObject);
        }
    }
}