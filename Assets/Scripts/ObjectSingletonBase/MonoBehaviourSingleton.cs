using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>, ISingleton
{
    [SerializeField, HideInInspector]
    private bool hasBeenValidated = false;

    public Object This { get { return this; } }

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Singletons.GetInstance<T>();
            }

            return instance;
        }
    }

    private void OnValidate()
    {
        if (!hasBeenValidated)
        {
#if UNITY_EDITOR

            if (!Application.isPlaying)
            {
                Object[] objs = Resources.LoadAll("Singletons", typeof(T));
                if (objs.Length > 1 && gameObject.scene.name != null)
                {
                    var array = new int[objs.Length - 1];

                    for (int i = 1; i < objs.Length; i++)
                    {
                        array[i - 1] = objs[i].GetInstanceID();
                    }

                    WarningWindow.ShowWindow("Can't have copies of the singleton either in the scene or in resources " + typeof(T).ToString(), new Object[1] { gameObject }, array, true);
                }
                else if (gameObject.scene.name != null)
                {
                    if (!Application.isPlaying)
                    {
                        WarningWindow.ShowWindow("A singleton can't start in a scene", new Object[1] { gameObject });
                    }
                }
                else if (objs.Length > 1)
                {
                    var array = new int[objs.Length - 1];

                    for (int i = 1; i < objs.Length; i++)
                    {
                        array[i - 1] = objs[i].GetInstanceID();
                    }

                    WarningWindow.ShowWindow("Can't have copies of the singleton " + typeof(T).ToString(), array, true);
                }
            }

#endif
        }

        hasBeenValidated = false;
    }

    private void Reset()
    {
        if (gameObject.scene.name != null)
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("A singleton can't start in a scene move it to the Resources/Singletons folder");
                hasBeenValidated = true;
            }

            if (transform != transform.root)
            {
#if UNITY_EDITOR
                WarningWindow.ShowWindow("A singleton needs to be on the top gameobject of a hierarchy", new Object[1] { gameObject });
#endif
            }
        }
    }
}