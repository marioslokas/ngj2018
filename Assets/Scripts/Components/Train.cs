using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    private Vector3 direction;
    private bool hasBeenStoopped;
    private bool stopped = false;

    public void Init(Vector3 direction)
    {
        this.direction = direction;
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
            //body.velocity = direction * Time.deltaTime * speed;
            transform.Translate(direction * Time.deltaTime * speed, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopSign"))
        {
            stopped = true;
            hasBeenStoopped = true;
            TrainSpawnManager.Instance.StartedHolding = true;
        }
        // TODO: ensure we destroy at some point.
        else if (hasBeenStoopped && other.CompareTag("DestroyTrain"))
        {
            Destroy(gameObject);
        }
    }
}