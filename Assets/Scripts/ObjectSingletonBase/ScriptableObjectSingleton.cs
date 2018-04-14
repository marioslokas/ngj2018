using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>, ISingleton
{
    public Object This { get { return this; } }

    private T instance;

    public T Instance
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

    private void Awake()
    {
#if UNITY_EDITOR
        Object[] objs = Resources.LoadAll("Singletons", typeof(T));

        if (objs.Length > 1)
        {
            var array = new int[objs.Length - 1];

            for (int i = 1; i < objs.Length; i++)
            {
                array[i - 1] = objs[i].GetInstanceID();
            }

            WarningWindow.ShowWindow("Can't have copies of a singleton " + typeof(T).ToString(), array, true);
        }
#endif
    }
}