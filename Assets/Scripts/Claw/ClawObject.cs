using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class ClawObject : MonoBehaviour
{
    public Rigidbody ClawBody;

    [HideInInspector] public bool IsGrabbing;

	[SerializeField]
	private float grabbingRange = 5f;

    private float m_DesiredHeight = 0;

	private List<GameObject> caughtPeople;

	void Start()
	{
		caughtPeople = new List<GameObject> ();
	}

    private void Awake()
    {
        m_DesiredHeight = transform.parent.position.y;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(transform.position.x, m_DesiredHeight, transform.position.z),
            Time.deltaTime * 2);

        if (IsGrabbing)
        {
            if (Mathf.Abs(transform.position.y - m_DesiredHeight) < 0.1f)
            {
                GatherPeople();
                m_DesiredHeight = transform.parent.position.y;
                IsGrabbing = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (caughtPeople.Count > 0)
            {
                ReleasePeople();
            }
            else
            {
                RaycastHit info;
                if (!Physics.Raycast(transform.position, Vector3.down, out info, 20))
                {
                    return;
                }

                m_DesiredHeight = transform.position.y - info.distance;
                IsGrabbing = true;
            }
        }
    }

    public void GatherPeople()
    {
		Collider[] peopleHit = Physics.OverlapSphere (this.transform.position, grabbingRange);

		for (int i = 0; i < peopleHit.Length; i++) {
			if (peopleHit [i].gameObject.tag.Equals("Person")) {
				AttachPersonToCrane (peopleHit [i]);
            }
		}
    }

	void AttachPersonToCrane(Collider person)
	{
		person.GetComponent<PersonBehavior> ().isOnCrane = true;
		person.GetComponent<PersonBehavior> ().hasBeenGrabbed = true;
		person.transform.SetParent (this.transform);
		person.GetComponent<Rigidbody> ().isKinematic = true;
		person.GetComponent<Rigidbody> ().useGravity = false;
		person.GetComponent<NavMeshAgent> ().enabled = false;
		caughtPeople.Add (person.transform.gameObject);
	}


    public void ReleasePeople()
    {
        for (int i = caughtPeople.Count - 1; i >= 0; i--)
        {
            var go = caughtPeople[i];
            go.GetComponent<PersonBehavior>().isOnCrane = false;
            go.transform.SetParent(null);
            var body = go.GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.useGravity = true;
            body.velocity = ClawBody.velocity * 1.1f;
        }
        caughtPeople.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, Vector3.down * m_DesiredHeight);
        RaycastHit info;
        if (!Physics.Raycast(transform.position, Vector3.down, out info, 20))
        {
            return;
        }
        Gizmos.DrawWireSphere(info.point, grabbingRange);
    }
}