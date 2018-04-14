using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Claw : MonoBehaviour
{
    public Rigidbody ClawBody;
    public ClawObject ClawObject;

    public float Speed = 12f;
    public float Drag = 0.97f;
    public float CameraDistance = 15f;
    public float CameraSpeed = 4f;

    public Rect ClawZone = Rect.zero;

    private Vector3 m_ClawVeloctiy = Vector3.zero;
    private Camera m_Camera;

    private void Awake()
    {
        Debug.Assert(ClawBody != null, "Claw behavior is missing its rigidbody", this);
        Debug.Assert(ClawObject != null, "Claw behavior is missing its claw object", this);
        m_Camera = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // TEMP debug code:
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = (CursorLockMode)(((int)++Cursor.lockState) % 2);
        }

        DoMove();
        DoCameraFollow();
    }

    private void DoMove()
    {
        float x = 0;
        float y = 0;

        if (!ClawObject.IsGrabbing)
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
        }

        m_ClawVeloctiy = new Vector3(m_ClawVeloctiy.x + x * Speed * Time.deltaTime, 0, 
                                     m_ClawVeloctiy.z + y * Speed * Time.deltaTime);

        ClawBody.velocity = m_ClawVeloctiy;
        m_ClawVeloctiy = m_ClawVeloctiy * Drag;

        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, ClawZone.xMin, ClawZone.xMax),
                                              transform.localPosition.y,
                                              Mathf.Clamp(transform.localPosition.z, ClawZone.yMin, ClawZone.yMax));
    }

    private void DoCameraFollow()
    {
        Vector3 desiredCameraPos = m_Camera.transform.forward * -CameraDistance + ClawBody.position;
        m_Camera.transform.localPosition = Vector3.Lerp(
            m_Camera.transform.localPosition, desiredCameraPos,
            Time.deltaTime * CameraSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = ClawZone.center;
        Vector3 size = ClawZone.size;
        Gizmos.DrawWireCube(new Vector3(center.x, 2, center.y), new Vector3(size.x, 4, size.y));
    }
}
