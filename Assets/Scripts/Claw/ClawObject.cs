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

    private List<GameObject> caughtPeople = new List<GameObject>();
    private List<GameObject> caughtOther = new List<GameObject>();

    private void Awake()
    {
        m_DesiredHeight = transform.parent.position.y;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(transform.position.x, m_DesiredHeight, transform.position.z),
            Time.deltaTime * 3);

        if (IsGrabbing)
        {
            if (Mathf.Abs(transform.position.y - m_DesiredHeight) < 0.35f)
            {
                GatherPeople();
                m_DesiredHeight = transform.parent.position.y;
                IsGrabbing = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
			if (caughtPeople.Count > 0 || caughtOther.Count > 0)
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

                m_DesiredHeight = transform.position.y - info.distance + 0.4f;
                IsGrabbing = true;
            }
        }
    }

    public void GatherPeople()
    {
		Collider[] peopleHit = Physics.OverlapSphere (this.transform.position, grabbingRange);

		for (int i = 0; i < peopleHit.Length; i++) {
			if (peopleHit [i].gameObject.CompareTag("Person")) {
				AttachPersonToCrane (peopleHit [i]);
            }
            else if (peopleHit[i].gameObject.CompareTag("TrainRoof"))
            {
                caughtOther.Add(peopleHit[i].gameObject);

                Rigidbody body = peopleHit[i].GetComponent<Rigidbody>();
                body.isKinematic = true;
                body.useGravity = false;
				peopleHit [i].transform.SetParent (this.transform);
            }
		}
    }

	void AttachPersonToCrane(Collider person)
    {
        person.transform.SetParent(this.transform);

        person.transform.position = Vector3.MoveTowards(person.transform.position, transform.position, 0.3f);

        PersonBehavior pb = person.GetComponent<PersonBehavior>();
        pb.isOnCrane = true;
		pb.hasBeenGrabbed = true;
        pb.agent.enabled = false;

        Rigidbody body = person.GetComponent<Rigidbody>();
        body.isKinematic = true;
		body.useGravity = false;

		caughtPeople.Add(person.transform.gameObject);
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
            body.velocity = ClawBody.velocity * 0.8f;
        }
        caughtPeople.Clear();


        for (int i = caughtOther.Count - 1; i >= 0; i--)
        {
			var go = caughtOther[i];
            go.transform.SetParent(null);

            var body = go.GetComponent<Rigidbody>();
            body.isKinematic = false;
            body.useGravity = true;
            body.velocity = ClawBody.velocity * 1.15f;
        }
        caughtOther.Clear();
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