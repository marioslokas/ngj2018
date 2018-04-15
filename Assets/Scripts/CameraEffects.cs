using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private static CameraEffects sInstance = null;
    public Camera TargetCamera;

    private void Start()
    {
        Debug.Assert(TargetCamera != null, "Camera Effects Manager is missing a target camera", this);

        if (sInstance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            sInstance = this;
        }
    }

    public static void RandomShake(int shakes, float freuqency, float scale)
    {
        sInstance.StartCoroutine(Shake(shakes, freuqency, scale));
    }

    private static IEnumerator Shake(int shakes, float freuqency, float scale)
    {
        int count = 0;
        WaitForSeconds shakeBounce = new WaitForSeconds(freuqency * 0.5f);
        while (count < shakes)
        {
            Vector3 startPos = sInstance.TargetCamera.transform.position;
            Vector2 circle = Random.insideUnitCircle;
            Vector3 random = new Vector3(circle.x * scale, circle.y * scale, 0);
            sInstance.TargetCamera.transform.Translate(random);
            yield return shakeBounce;
            sInstance.TargetCamera.transform.position = startPos;
            yield return shakeBounce;
            count++;
        }
    }
}