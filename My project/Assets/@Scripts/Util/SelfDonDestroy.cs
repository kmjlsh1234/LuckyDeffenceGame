using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDonDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
