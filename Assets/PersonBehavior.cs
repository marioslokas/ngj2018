using UnityEngine.AI;
using UnityEngine;

public class PersonBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject platform;

    private bool isOnPlatform;
    private bool isOnCrane;
    private bool isOnTrain;

    public Shapes MyShape { get; private set; }

    private void Awake()
    {
        MyShape = (Shapes)Random.Range(1, 4);

        Debug.Log(MyShape);
    }

    // Use this for initialization
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        agent.Warp(other.contacts[0].point);

        if (other.collider.gameObject.tag.Equals("Platform"))
        {
            isOnPlatform = true;
            Walk();
        }
        else if (other.collider.gameObject.tag.Equals("Person"))
        {
            // Walk();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.gameObject.tag.Equals("Platform"))
        {
            isOnPlatform = false;
        }
    }

    private void Walk()
    {
        agent.isStopped = true;

        agent.SetDestination(GetRandomPosition());

        agent.isStopped = false;
    }

    private Vector3 GetRandomPosition()
    {
        //Random.InitState(System.DateTime.Now.Millisecond);

        float randomX = Random.Range(platform.transform.position.x - platform.transform.localScale.x / 2,
                    platform.transform.position.x + platform.transform.localScale.x / 2);

        float randomZ = Random.Range(platform.transform.position.y - platform.transform.localScale.z / 2,
            platform.transform.position.y + platform.transform.localScale.z / 2);

        return new Vector3(randomX, 0, randomZ);
    }