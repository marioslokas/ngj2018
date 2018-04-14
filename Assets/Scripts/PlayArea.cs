using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Data/Play Area")]
public class PlayArea : ScriptableObject
{
    public Rect Area;

    public Vector3 ClampVectorToArea(Vector3 v)
    {
        return new Vector3(Mathf.Clamp(v.x, Area.xMin, Area.xMax),
                           v.y,
                           Mathf.Clamp(v.z, Area.yMin, Area.yMax));
    }

    public bool Contains(Vector3 v)
    {
        return v.x < Area.xMax
            && v.x > Area.xMin
            && v.z < Area.yMax
            && v.z > Area.yMin;
    }

#if UNITY_EDITOR
    public void DrawGizmo()
    {
        Vector3 center = Area.center;
        Vector3 size = Area.size;
        Gizmos.DrawWireCube(new Vector3(center.x, 5, center.y), new Vector3(size.x, 10, size.y));
    }
#endif
}