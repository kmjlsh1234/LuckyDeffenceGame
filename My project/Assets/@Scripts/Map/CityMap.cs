using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class CityMap : MapBase
{
    public override void Init()
    {
        base.Init();
        _mapType = MapType.Map_City;
    }
}
