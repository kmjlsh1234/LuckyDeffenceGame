using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    private BoxCollider _collider;

    public void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public bool IsEmpty()
    {
        return (transform.childCount == 0) ? true : false; 
    }
}
