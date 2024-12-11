using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HeroState<T>
{
    public void Idle(T t);

    public void Move(T t);

    public void Attack(T t);

}
