using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    #region ::::USERS
    public Users users = new Users();
    public LoginViewModel loginViewModel;
    #endregion

    public List<HeroProbability> heroProbabilities = new List<HeroProbability>();

    public List<HeroData> heroDatas = new List<HeroData>();
    public List<EnemyData> enemyDatas = new List<EnemyData>();
    public List<MapPosData> mapPosDatas = new List<MapPosData>();

    
    private int heroTypeEnumCount;

    public void Init()
    {
        heroTypeEnumCount = System.Enum.GetValues(typeof(HeroType)).Length;

        heroProbabilities.Clear();
        heroDatas.Clear();
        enemyDatas.Clear();
        mapPosDatas.Clear();

        LoadData("LoginViewModel", ref loginViewModel);
        LoadListData("HeroProbability", ref heroProbabilities);
        LoadListData("HeroData", ref heroDatas);
        LoadListData("EnemyData", ref enemyDatas);
        LoadListData("MapPosData", ref mapPosDatas);
    }

    public void LoadData<T>(string fileName, ref T data)
    {
        TextAsset asset = Resources.Load<TextAsset>(Constant.DATA_FILE_PATH + fileName);
        if (asset != null)
        {
            data = JsonConvert.DeserializeObject<T>(asset.text);
            Debug.Log($"Data {fileName} Load Complete!");
        }
        else
        {
            Debug.LogError($"Data {fileName} Not Exist!");
        }
    }

    public void LoadListData<T>(string fileName, ref List<T> list)
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
        var data = heroDatas.FirstOrDefault(x => x.PrefabName.Equals(prefabName));
        return (data != null) ? data : null;
    }

    public EnemyData FindEnemyDataByPrefabName(string prefabName)
    {
        
        var data = enemyDatas.FirstOrDefault(x => x.PrefabName.Equals(prefabName));
        return (data != null) ? data : null;
    }

    public string SelectRandomHeroByProbability()
    {
        string targetName = string.Empty;
        

        //TODO : ��� Ȯ�� üũ
        float randomPoint = Random.Range(0f, 100f);
        float cumulativeProbability = 0f;

        string gradeType = string.Empty;
        foreach (var heroProbability in heroProbabilities)
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
        string heroType = System.Enum.GetName(typeof(HeroType), randomType);

        //TODO : �ش� ����� Ÿ���� ���� �� üũ

        //TODO : ���ڿ� ����
        targetName += heroType;
        targetName += "_";
        targetName += gradeType;
        return targetName;
    }

}
