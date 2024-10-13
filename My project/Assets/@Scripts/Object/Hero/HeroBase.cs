using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
    private Animator _anim;
    private CircleCollider2D _attackRange;
    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _attackRange = GetComponent<CircleCollider2D>();
    }
}
