using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class PersonBehavior : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;
    public GameObject platform;

    private bool isOnPlatform;
    [HideInInspector] public bool isOnCrane;
    internal bool isOnTrain;
    [HideInInspector] public bool hasBeenGrabbed;

    public GameObject[] m_Visuals;

    public float walkRadius = 10f;

    public Shapes MyShape;
    public float walkFrequency = 6f;

    private float currentTime = 0f;
    private Quaternion rotationUp;

    [Header("Wiggling parameters")]
    public AnimationCurve curve;

    public Vector3 distance;
    public float speed;

    private Vector3 startPos, toPos;
    private float timeStart;

    private float layDownTime = 2f;

    private bool standingUp;

    public AudioSource AudioThump;

    // Use this for initialization
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.Warp(this.transform.position);

        startPos = transform.position;
        randomToPos();
        isOnPlatform = true;

        rotationUp = new Quaternion(0, 1, 0, 0);

        Debug.Assert(Mathf.Clamp((int)MyShape, 2, 4) == (int)MyShape, "Person Behavior receives the wrong shape.", this);

        m_Visuals[(int)MyShape - 2].SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isOnCrane || isOnTrain || hasBeenGrabbed)
        {
            return;
        }

        if (agent.pathPending)
        {
            return;
        }

        currentTime += Time.deltaTime;
        if (!isOnCrane && !isOnTrain)
        {
            if (currentTime > walkFrequency)
            {
                Walk();
                currentTime = 0f;
            }
        }

        CheckForFall();
    }

    private void CheckForFall()
    {
        if (Vector3.Angle(this.transform.up, Vector3.up) > 70)
        {
            if (!standingUp)
            {
                StartCoroutine(StandBackUp());
                standingUp = true;
            }
        }
    }

    private IEnumerator StandBackUp()
    {
        yield return new WaitForSeconds(layDownTime);
        Debug.Log("STANDING UP");
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationUp, t);
            yield return null;
        }
        standingUp = false;
    }

    private void Wiggle()
    {
        float d = (Time.time - timeStart) / speed, m = curve.Evaluate(d);
        if (d > 1)
        {
            randomToPos();
        }
        else if (d < 0.5)
        {
            transform.position = Vector3.Lerp(startPos, toPos, m * 2.0f);
        }
        else
        {
            transform.position = Vector3.Lerp(toPos, startPos, (m - 0.5f) * 2.0f);
        }
    }

    private void randomToPos()
    {
        toPos = startPos;
        toPos.x += Random.Range(-1.0f, +1.0f) * distance.x;
        toPos.y += Random.Range(-1.0f, +1.0f) * distance.y;
        toPos.z += Random.Range(-1.0f, +1.0f) * distance.z;
        timeStart = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("TrainArea"))
        {
            isOnTrain = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenGrabbed)
        {
            AudioThump.pitch = Random.Range(0.3f, 1.9f);
            AudioThump.Play();
        }
    }

    private void Walk()
    {
        agent.isStopped = true;

        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);

        agent.isStopped = false;
    }
}