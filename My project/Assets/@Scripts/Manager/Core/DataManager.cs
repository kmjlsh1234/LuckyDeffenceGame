using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    public List<HeroProbability> _heroProbabilities = new List<HeroProbability>();

    private int heroTypeEnumCount;
    public void Init()
    {
        heroTypeEnumCount = System.Enum.GetValues(typeof(Enum.HeroType)).Length;
        LoadHeroProbabilityData();
    }

    public void LoadHeroProbabilityData()
    {
        TextAsset asset = Resources.Load<TextAsset>(Constant.DATA_FILE_PATH + "HeroProbability");
        if(asset != null)
        {
            _heroProbabilities = JsonConvert.DeserializeObject<List<HeroProbability>>(asset.text);
        }
        else
        {
            Debug.LogError("Data Not Exist!");
        }
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
        string heroType = System.Enum.GetName(typeof(Enum.HeroType), randomType);

        //TODO : 해당 등급의 타입의 종류 수 체크

        //TODO : 문자열 조합
        targetName += gradeType;
        targetName += "_";
        targetName += heroType;
        return targetName;
    }

}
