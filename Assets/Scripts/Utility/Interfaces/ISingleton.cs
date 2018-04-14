using UnityEngine;

public interface ISingleton
{
    Object This { get; }

    void Init();
}