using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject() { name = $"@{typeof(T).Name}" };
                instance = go.AddComponent<T>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
}
