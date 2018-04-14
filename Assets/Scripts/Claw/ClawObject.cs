using UnityEngine;
using System.Collections;

public class ClawObject : MonoBehaviour
{
    [HideInInspector] public bool IsGrabbing;
    [HideInInspector] public bool TempHasPeople;

    private float m_DesiredHeight = 0;

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

	IEnumerator TryAndGrab()
	{
		Animator.SetTrigger(TempHasPeople ? kAnimatorRelease : kAnimatorGrab);
		yield return null;
		yield return Animator.GetCurrentAnimatorStateInfo (0).length;

		Collider[] peopleInRange = Physics.OverlapSphere (this.transform.position, 2f);

		if (peopleInRange.Length != 0) {
			// grab
		}


	}

    public void GatherPeople()
    {
        Debug.Log("Grabbing people");
        TempHasPeople = true;
    }

    public void ReleasePeople()
    {
        Debug.Log("Releasing people");
        TempHasPeople = false;
    }
}