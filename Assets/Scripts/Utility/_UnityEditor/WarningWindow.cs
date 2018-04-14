#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public class WarningWindow : EditorWindow
{
    private bool isDestroyed;

    private static string msg;
    private static Object[] objs;
    private static int[] assetIDs;
    private static EditorWindow window;
    private static bool shouldDestroyByID;

    public static void ShowWindow(string message, Object[] objsToBeDestroyed)
    {
        msg = message;
        objs = objsToBeDestroyed;

        window = GetWindow(typeof(WarningWindow));

        window.maxSize = new Vector2(500, 70);
        window.minSize = window.maxSize;
    }

    public static void ShowWindow(string message, Object[] objsToBeDestoyed, int[] assetInstanceID, bool destroyByID)
    {
        msg = message;
        objs = objsToBeDestoyed;
        assetIDs = assetInstanceID;
        shouldDestroyByID = true;
        window = GetWindow(typeof(WarningWindow));

        window.maxSize = new Vector2(500, 70);
        window.minSize = window.maxSize;
    }

    public static void ShowWindow(string message, int[] assetInstanceIDs, bool destroyByID)
    {
        msg = message;
        assetIDs = assetInstanceIDs;
        shouldDestroyByID = destroyByID;
        window = GetWindow(typeof(WarningWindow));

        window.maxSize = new Vector2(500, 70);
        window.minSize = window.maxSize;
    }

    private void OnGUI()
    {
        EditorGUILayout.TextField(msg);

        for (int i = 0; i < 4; i++)
        {
            EditorGUILayout.Space();
        }

        GUILayout.BeginArea(new Rect((Screen.width / 2) - 50, 50, 100, 100));

        if (GUILayout.Button("Close"))
        {
            window.Close();
        }

        GUILayout.EndArea();
    }

    private void OnLostFocus()
    {
        if (!isDestroyed && objs[0] && shouldDestroyByID)
        {
            isDestroyed = true;

            for (int i = 0; i < objs.Length; i++)
            {
                DestroyImmediate(objs[i], true);
            }

            for (int i = 0; i < assetIDs.Length; i++)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(assetIDs[i]));
            }
        }
        else if (!isDestroyed && objs[0])
        {
            isDestroyed = true;

            for (int i = 0; i < objs.Length; i++)
            {
                DestroyImmediate(objs[i], true);
            }
        }
        else if (!isDestroyed && shouldDestroyByID)
        {
            isDestroyed = true;

            for (int i = 0; i < assetIDs.Length; i++)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(assetIDs[i]));
            }
        }
    }
}

#endif