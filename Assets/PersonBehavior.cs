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

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision other)
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
		Bounds b = thisRenderer.bounds;

		float randomX = Random.Range(b.min.x, b.max.x);

		float randomZ = Random.Range(b.min.y, b.max.y);

        return new Vector3(randomX, 0, randomZ);
    }
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