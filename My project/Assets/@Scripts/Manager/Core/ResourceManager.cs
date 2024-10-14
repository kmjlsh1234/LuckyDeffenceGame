using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonBase<ResourceManager>
{
    private Dictionary<string, GameObject> _heroDic = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> _enemyDic = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> _projectileDic = new Dictionary<string, GameObject>();

    public void Init()
    {
        LoadData(_heroDic, Constant.HERO_PREFAB_PATH);
        LoadData(_enemyDic, Constant.ENEMY_PREFAB_PATH);
        LoadData(_projectileDic, Constant.PROJECTILE_PREFAB_PATH);
    }

    public void LoadData<T>(Dictionary<string,T> dic, string filePath) where T : UnityEngine.Object
    {
        T[] array = Resources.LoadAll<T>(filePath);
        foreach(T asset in array)
        {
            dic.Add(asset.name, asset);
        }    
    }

    public GameObject GetHero(string key)
    {
        GameObject hero = null;
        
        if(_heroDic.TryGetValue(key, out hero))
        {
            return hero;
        }
        else
        {
            Debug.LogError($"{key} Not Exist!");
            return null;
        }
    }

    public GameObject GetEnemy(string key)
    {
        GameObject enemy = null;

        if(_enemyDic.TryGetValue(key, out enemy))
        {
            return enemy;
        }
        else
        {
            Debug.LogError($"{key} Not Exist!");
            return null;
        }
    }

    public GameObject GetProjectile(string key)
    {
        GameObject projectile = null;

        if (_projectileDic.TryGetValue(key, out projectile))
        {
            return projectile;
        }
        else
        {
            Debug.LogError($"{key} Not Exist!");
            return null;
        }
    }
}
