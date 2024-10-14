using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

[System.Serializable]
public class HeroData
{
    public int ID;
    public string PrefabName;
    public string Name;
    public Grade Grade;
    public HeroType HeroType;
    public AttackType AttackType;
    public float Damage;
    public float AttackSpeed;
    public float AttackRange;
}
