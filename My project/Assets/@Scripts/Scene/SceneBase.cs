using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    protected SceneName _sceneName;
    public virtual void Init()
    {
        Debug.Log($"{_sceneName.ToString()} is load!");
    }
}
