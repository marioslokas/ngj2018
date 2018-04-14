using UnityEngine.AI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehavior : MonoBehaviour {

    private NavMeshAgent agent;

    public GameObject platform;

	bool isOnPlatform;
	bool isOnCrane;
	bool isOnTrain;

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

	void OnCollisionExit(Collision other)
	{
		if (other.collider.gameObject.tag.Equals("Platform")) {
			isOnPlatform = false;
		}
	}

    void Walk()
    {
        agent.isStopped = true;

        agent.SetDestination(GetRandomPosition());

        agent.isStopped = false;
    }

    Vector3 GetRandomPosition()
    {
        //Random.InitState(System.DateTime.Now.Millisecond);

        float randomX = Random.Range(platform.transform.position.x - platform.transform.localScale.x / 2,
                    platform.transform.position.x + platform.transform.localScale.x / 2);

        float randomZ = Random.Range(platform.transform.position.y - platform.transform.localScale.z / 2,
            platform.transform.position.y + platform.transform.localScale.z / 2);

        return new Vector3(randomX, 0, randomZ);
    }

}
