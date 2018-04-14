using System;
using System.Collections.Generic;
using UnityEngine;

public static class Singletons
{
    private static List<ISingleton> instances = new List<ISingleton>();
    private static List<ISingleton> singletonResources = new List<ISingleton>();

    static Singletons()
    {
        var array = Resources.LoadAll("Singletons", typeof(ISingleton));

        for (int i = 0; i < array.Length; i++)
        {
            singletonResources.Add((ISingleton)array[i]);
        }
    }

    internal static T GetInstance<T>() where T : UnityEngine.Object, ISingleton
    {
        T instance;

        if ((instance = (T)instances.Find(i => i.GetType() == typeof(T))) != null)
        {
            return instance;
        }
        else
        {
            instance = (UnityEngine.Object.Instantiate(singletonResources.Find(i => i.GetType() == typeof(T)).This) as T);
            instances.Add(instance);
            UnityEngine.Object.DontDestroyOnLoad(instance.This);
            instance.Init();
            return instance;
        }
    }
}