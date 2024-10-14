using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    private BoxCollider _collider;

    public bool IsEmpty { get { return _isEmpty; } set { _isEmpty = value; } }
    private bool _isEmpty = true;

    public void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
}
