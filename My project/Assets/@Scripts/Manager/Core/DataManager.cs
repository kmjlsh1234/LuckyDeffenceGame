using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    public List<HeroProbability> _heroProbabilities = new List<HeroProbability>();

    public List<HeroData> _heroDatas = new List<HeroData>();
    public List<EnemyData> _enemyDatas = new List<EnemyData>();
    public List<MapPosData> _mapPosDatas = new List<MapPosData>();

    private int heroTypeEnumCount;

    public void Init()
    {
        heroTypeEnumCount = System.Enum.GetValues(typeof(HeroType)).Length;

        _heroProbabilities.Clear();
        _heroDatas.Clear();
        _enemyDatas.Clear();
        _mapPosDatas.Clear();

        LoadData("HeroProbability", ref _heroProbabilities);
        LoadData("HeroData", ref _heroDatas);
        LoadData("EnemyData", ref _enemyDatas);
        LoadData("MapPosData", ref _mapPosDatas);
    }

    public void LoadData<T>(string fileName, ref List<T> list)
    {
        TextAsset asset = Resources.Load<TextAsset>(Constant.DATA_FILE_PATH + fileName);
        if(asset != null)
        {
            list = JsonConvert.DeserializeObject <List<T>> (asset.text);
            Debug.Log($"Data {fileName} Load Complete!");
        }
        else
        {
            Debug.LogError($"Data {fileName} Not Exist!");
        }
    }

    public HeroData FindHeroDataByPrefabName(string prefabName)
    {
        var data = _heroDatas.FirstOrDefault(x => x.PrefabName.Equals(prefabName));
        return (data != null) ? data : null;
    }

    public EnemyData FindEnemyDataByPrefabName(string prefabName)
    {
        
        var data = _enemyDatas.FirstOrDefault(x => x.PrefabName.Equals(prefabName));
        return (data != null) ? data : null;
    }

    public string SelectRandomHeroByProbability()
    {
        string targetName = string.Empty;
        

        //TODO : 등급 확률 체크
        float randomPoint = Random.Range(0f, 100f);
        float cumulativeProbability = 0f;

        string gradeType = string.Empty;
        foreach (var heroProbability in _heroProbabilities)
        {
            cumulativeProbability += heroProbability.Probability;
            if(randomPoint <= cumulativeProbability)
            {
                gradeType = heroProbability.Grade;
                break;
            }
        }

        //TODO : 타입 체크
        int randomType = Random.Range(0, heroTypeEnumCount);
        string heroType = System.Enum.GetName(typeof(HeroType), randomType);

        //TODO : 해당 등급의 타입의 종류 수 체크

        //TODO : 문자열 조합
        targetName += heroType;
        targetName += "_";
        targetName += gradeType;
        return targetName;
    }

}
