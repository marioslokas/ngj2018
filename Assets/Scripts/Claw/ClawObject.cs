using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class ClawObject : MonoBehaviour
{
    public Rigidbody ClawBody;

    public Animator Claw1;
    public Animator Claw2;
    public Animator Claw3;

    public AudioSource AudioClawOpen;
    public AudioSource AudioClawClose;

    public AudioSource AudioOpenLid;
    public AudioSource AudioSwuzh;

    public AudioClip[] AudioReactions;
    public AudioSource AudioReaction;

    [HideInInspector] public bool IsGrabbing;

    [SerializeField]
    private float grabbingRange = 5f;

    private float m_DesiredHeight = 0;

    private List<PersonBehavior> caughtPeople = new List<PersonBehavior>();
    private List<Rigidbody> caughtOther = new List<Rigidbody>();

    private void Awake()
    {
        m_DesiredHeight = transform.parent.position.y;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(transform.position.x, m_DesiredHeight, transform.position.z),
            Time.deltaTime * 3);

        //Claw1.SetBool("IsOpen", IsGrabbing);
        //Claw2.SetBool("IsOpen", IsGrabbing);
        //Claw3.SetBool("IsOpen", IsGrabbing);

        if (IsGrabbing)
        {
            Claw1.SetTrigger("Grab");
            Claw2.SetTrigger("Grab");
            Claw3.SetTrigger("Grab");

            if (Mathf.Abs(transform.position.y - m_DesiredHeight) < 0.35f)
            {
                AudioClawClose.Play();
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

                AudioClawOpen.Play();

                m_DesiredHeight = transform.position.y - info.distance;
                IsGrabbing = true;
            }
        }
    }

    public void GatherPeople()
    {
        Collider[] thingsHit = Physics.OverlapSphere(transform.position, grabbingRange, LayerMask.GetMask("People"));

        for (int i = 0; i < thingsHit.Length; i++)
        {
            if (thingsHit[i].CompareTag("Person"))
            {
                AttachPersonToCrane(thingsHit[i].transform.parent);
            }
            else if (thingsHit[i].CompareTag("TrainRoof"))
            {
                AudioOpenLid.Play();

                Rigidbody body = thingsHit[i].GetComponent<Rigidbody>();
                body.isKinematic = true;
                body.useGravity = false;
                thingsHit[i].transform.SetParent(this.transform);

                caughtOther.Add(body);
            }
        }
    }

    private void AttachPersonToCrane(Transform person)
    {
        if (Random.Range(0, 5) == 4)
        {
            AudioReaction.clip = AudioReactions[Random.Range(0, AudioReactions.Length)];
            AudioReaction.Play();
        }

        person.transform.position = Vector3.MoveTowards(person.transform.position, transform.position, 0.3f);

        PersonBehavior pb = person.GetComponent<PersonBehavior>();
        pb.isOnCrane = true;
        pb.isOnTrain = false;
        pb.hasBeenGrabbed = true;
        pb.agent.enabled = false;

        pb.Body.isKinematic = true;
        pb.Body.useGravity = false;
        person.transform.SetParent(this.transform);

        caughtPeople.Add(pb);
    }

    public void ReleasePeople()
    {
        var addedVelocity = ClawBody.velocity * 0.8f;

        if (addedVelocity.sqrMagnitude > 20 * 20)
        {
            AudioSwuzh.Play();
        }

        for (int i = caughtPeople.Count - 1; i >= 0; i--)
        {
            var pb = caughtPeople[i];
            pb.isOnCrane = false;
            pb.transform.SetParent(null);

            pb.Body.isKinematic = false;
            pb.Body.useGravity = true;
            pb.Body.velocity = addedVelocity;
        }
        caughtPeople.Clear();

        for (int i = caughtOther.Count - 1; i >= 0; i--)
        {
            var rb = caughtOther[i];
            rb.transform.SetParent(null);
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = addedVelocity;
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
        Gizmos.DrawWireSphere(transform.position, grabbingRange);
    }
}