using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonBase<CameraManager>
{
    public Camera MainCamera;

    public void Init()
    {
        MainCamera = Camera.main;
        MainCamera.transform.SetParent(this.transform);
    }
}
