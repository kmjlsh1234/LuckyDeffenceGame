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
        

        //TODO : ��� Ȯ�� üũ
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

        //TODO : Ÿ�� üũ
        int randomType = Random.Range(0, heroTypeEnumCount);
        string heroType = System.Enum.GetName(typeof(Enum.HeroType), randomType);

        //TODO : �ش� ����� Ÿ���� ���� �� üũ

        //TODO : ���ڿ� ����
        targetName += gradeType;
        targetName += "_";
        targetName += heroType;
        return targetName;
    }

}
