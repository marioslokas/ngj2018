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

    private void Start()
    {
        MyShape = (Shapes)UnityEngine.Random.Range(1, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        PersonBehavior pb;

        if ((pb = other.gameObject.GetComponent<PersonBehavior>()) != null)
        {
            if (MyShape == Shapes.All || MyShape == pb.MyShape)
            {
                pb.transform.SetParent(this.transform);
                pb.isOnTrain = true;
                return;
            }

            //			GameObject spawnPoint = GameObject.FindGameObjectWithTag ("PeopleSpawnPoint");
            //			pb.GetComponent<NavMeshAgent> ().Warp (spawnPoint.transform.position);
            //ToDo Panic or run out?
            Destroy(pb.gameObject);
        }
    }
}