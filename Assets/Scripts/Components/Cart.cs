using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cart : MonoBehaviour
{
    public Animator LeftDoor;
    public Animator RightDoor;

    [SerializeField]
    internal float CartLength = 1;

    internal Shapes MyShape;

	[SerializeField]
	private GameObject spawnPoint;

    public void SetDoors(bool isOpen)
    {
        LeftDoor.SetBool("IsOpen", isOpen);
        RightDoor.SetBool("IsOpen", isOpen);
    }

    private void Start()
	{
		MyShape = (Shapes)UnityEngine.Random.Range (2, 5);
	}

    private void OnTriggerEnter(Collider other)
    {
        PersonBehavior pb;

		if ((pb = other.gameObject.GetComponent<PersonBehavior>()) != null)
        {
			if (MyShape == Shapes.All || MyShape == pb.MyShape) {
				Debug.Log ("PERSON ABOARD");
                pb.transform.SetParent(this.transform);
				return;
			}

//			GameObject spawnPoint = GameObject.FindGameObjectWithTag ("PeopleSpawnPoint");
//			pb.GetComponent<NavMeshAgent> ().Warp (spawnPoint.transform.position);
			Debug.Log ("DIDN'T FIT");
            //ToDo Panic or run out?
            Destroy(pb.gameObject);
        }
    }
}