using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class MapManager : SingletonBase<MapManager>
{
    private Dictionary<string, GameObject> _mapDic = new Dictionary<string, GameObject>();

    public MapBase CurrentMap { get; set; }
    public MapType CurrentMapType { get; set; }
    public void Init()
    {
        _mapDic.Clear();

        GameObject[] maps = Resources.LoadAll<GameObject>(Constant.MAP_PREFAB_PATH);
        foreach (GameObject map in maps)
        {
            _mapDic.Add(map.name, map);
        }
    }

    public void GenerateMap()
    {
        GameObject go = null;
        if(_mapDic.TryGetValue(CurrentMapType.ToString(),out go))
        {
            GameObject map = Instantiate(go);
            map.transform.position = Vector3.zero;
            map.transform.rotation = Quaternion.identity;
            map.transform.localScale = Vector3.one;
            map.name.Replace("(Clone)", "");
            MapBase mapBase = map.GetComponent<MapBase>();
            CurrentMap = mapBase;
        }
        else
        {
            Debug.LogError($"{CurrentMapType.ToString()} Not Exist!");
        }

    }
}
