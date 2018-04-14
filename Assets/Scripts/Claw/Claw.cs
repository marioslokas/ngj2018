using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Claw : MonoBehaviour
{
    public Rigidbody ClawBody;
    public ClawObject ClawObject;
    public PlayArea ClawZone;

    public float Speed = 12f;
    public float Drag = 0.97f;
    public float CameraDistance = 15f;
    public float CameraSpeed = 4f;


    private Vector3 m_ClawVeloctiy = Vector3.zero;
    private Camera m_Camera;

    private void Awake()
    {
        Debug.Assert(ClawZone != null, "Claw behavior is missing its Play Area ref", this);
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
            float modif = Speed * Time.deltaTime;
            x = Input.GetAxis("Mouse X") * modif;
            y = Input.GetAxis("Mouse Y") * modif;
        }

        m_ClawVeloctiy = new Vector3(m_ClawVeloctiy.x + x, 0,
                                     m_ClawVeloctiy.z + y);

        ClawBody.velocity = m_ClawVeloctiy;
        m_ClawVeloctiy = m_ClawVeloctiy * Drag;

        transform.localPosition = ClawZone.ClampVectorToArea(transform.localPosition);
    }

    private void DoCameraFollow()
    {
        Vector3 desiredCameraPos = m_Camera.transform.forward * -CameraDistance + ClawBody.position;
        m_Camera.transform.localPosition = Vector3.Lerp(
            m_Camera.transform.localPosition, desiredCameraPos,
            Time.deltaTime * CameraSpeed);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ClawZone == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        ClawZone.DrawGizmo();
    }
#endif
}