using UnityEngine.AI;
using UnityEngine;

public class PersonBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject platform;

    private bool isOnPlatform;
    private bool isOnCrane;
    private bool isOnTrain;

	public float walkRadius = 10f;

    public Shapes MyShape { get; private set; }

	public float walkFrequency = 6f;
		
	public float currentTime = 0f;




    private void Awake()
	{
		MyShape = (Shapes)Random.Range (1, 4);
		Debug.Log(MyShape);
	}
	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
		agent.Warp (this.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

		currentTime += Time.deltaTime;
		if (!isOnCrane && !isOnTrain) {
			if (currentTime > walkFrequency) {
				Walk ();
				currentTime = 0f;
			}
		}

		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("TrainArea")) {
			isOnTrain = true;
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
		agent.SetDestination (finalPosition);

        agent.isStopped = false;
    }
		

}
