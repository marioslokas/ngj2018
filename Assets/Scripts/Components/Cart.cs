using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField]
    internal float CartLength = 1;

    internal Shapes MyShape;

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private SpriteRenderer m_ShapeIndicator;

    [SerializeField]
    private Sprite[] m_ShapeIndicators;

    private void Start()
    {
        MyShape = (Shapes)Random.Range(1, 5);
        m_ShapeIndicator.sprite = m_ShapeIndicators[(int)MyShape - 1];
    }

    private void OnTriggerEnter(Collider other)
    {
        PersonBehavior pb;

        if ((pb = other.GetComponent<PersonBehavior>()) != null)
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