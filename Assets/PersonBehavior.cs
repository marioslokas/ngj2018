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
		
	private float currentTime = 0f;

	[Header("Wiggling parameters")]
	public AnimationCurve curve;
	public Vector3 distance;
	public float speed;

	private Vector3 startPos, toPos;
	private float timeStart;


    private void Awake()
	{
		MyShape = (Shapes)Random.Range (1, 4);
		Debug.Log(MyShape);
	}
	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
		agent.Warp (this.transform.position);

		startPos = transform.position;
		randomToPos();
		isOnPlatform = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (agent.pathPending) {
			return;
		}
			
		currentTime += Time.deltaTime;
		if (!isOnCrane && !isOnTrain) {
			if (currentTime > walkFrequency) {
				Walk ();
				currentTime = 0f;
			}
		}
			
		
	}

	void Wiggle()
	{
		float d = (Time.time - timeStart) / speed, m = curve.Evaluate(d);
		if (d > 1) {
			randomToPos();
		} else if (d < 0.5) {
			transform.position = Vector3.Lerp(startPos, toPos, m * 2.0f);
		} else {
			transform.position = Vector3.Lerp(toPos, startPos, (m - 0.5f) * 2.0f);
		}
	}

	void randomToPos() {
		toPos = startPos;
		toPos.x += Random.Range(-1.0f, +1.0f) * distance.x;
		toPos.y += Random.Range(-1.0f, +1.0f) * distance.y;
		toPos.z += Random.Range(-1.0f, +1.0f) * distance.z;
		timeStart = Time.time;
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
