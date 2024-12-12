using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : SingletonBase<CameraManager>
{
    public Camera MainCamera;

    public void Init()
    {
        MainCamera = Camera.main;
        MainCamera.transform.SetParent(this.transform);
    }

    public GameObject RaycastTarget(Vector3 touchPos, string type)
    {
        Vector3 worldPoint = MainCamera.ScreenToWorldPoint(touchPos);
        RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);

        if(hits.Length ==0)
        {
            Debug.LogWarning("No Object Detected");
            return null;
        }
        else
        {
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag(type))
                {
                    return hit.collider.gameObject;
                }
            }
            Debug.LogWarning($"No {type} Object Detected");
            return null;
        }
    }
}
