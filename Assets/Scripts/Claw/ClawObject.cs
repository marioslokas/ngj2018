using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class ClawObject : MonoBehaviour
{
    [HideInInspector] public bool IsGrabbing;
    [HideInInspector] public bool TempHasPeople;

	[SerializeField]
	private float grabbingRange = 5f;

    private float m_DesiredHeight = 0;

	List<GameObject> caughtPeople;

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
            if (TempHasPeople)
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
        Debug.Log("Grabbing people");
        TempHasPeople = true;

		Collider[] peopleHit = Physics.OverlapSphere (this.transform.position, grabbingRange);

		if (peopleHit.Length !=0) {
			TempHasPeople = true;
		}

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
		foreach(GameObject go in caughtPeople)
		{
			go.GetComponent<PersonBehavior> ().isOnCrane = false;
			go.transform.SetParent (null);
			go.GetComponent<Rigidbody> ().isKinematic = false;
			go.GetComponent<Rigidbody> ().useGravity = true;
		}
		TempHasPeople = false;
    }
}