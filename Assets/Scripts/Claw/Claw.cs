using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Claw : MonoBehaviour
{
    public Rigidbody m_ClawBody;
    private Camera m_Camera;

    private Vector3 m_ClawVeloctiy = Vector3.zero;

    public float Speed = 12f;
    public float Drag = 0.97f;

    public Rect ClawZone = Rect.zero;

    private void Awake()
    {
        Debug.Assert(m_ClawBody != null, "Claw behavior is missing its rigidbody", this);
        m_Camera = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = (CursorLockMode)(((int)++Cursor.lockState) % 2);
        }

        //DoDetection();
        DoMove();
    }

    private void DoMove()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        m_ClawVeloctiy = new Vector3(m_ClawVeloctiy.x + x * Speed * Time.deltaTime, 0, 
                                     m_ClawVeloctiy.z + y * Speed * Time.deltaTime);

        m_ClawBody.velocity = m_ClawVeloctiy;
        m_ClawVeloctiy = m_ClawVeloctiy * Drag;

        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, ClawZone.xMin, ClawZone.xMax), transform.localPosition.y,
                                              Mathf.Clamp(transform.localPosition.z, ClawZone.yMin, ClawZone.yMax));
    }

#if false
    private void DoDetection()
    {
        Ray cameraRay = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (!Physics.Raycast(cameraRay, out info, 100))
        {
            return;
        }
    }
#endif

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = ClawZone.center;
        Vector3 size = ClawZone.size;
        Gizmos.DrawWireCube(new Vector3(center.x, 2, center.y), new Vector3(size.x, 4, size.y));
    }
}
