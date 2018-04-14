using UnityEngine;

public class ClawObject : MonoBehaviour
{
    public Animator Animator;

    private const string kAnimatorGrab = "Grab";
    private const string kAnimatorRelease = "Release";

    [HideInInspector] public bool IsGrabbing;
    [HideInInspector] public bool TempHasPeople;

    private void Awake()
    {
        Debug.Assert(Animator != null, "Claw Object is missing a reference to its Animator", this);
    }

    private void Update()
    {
        if (IsGrabbing)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Animator.SetTrigger(TempHasPeople ? kAnimatorRelease : kAnimatorGrab);
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