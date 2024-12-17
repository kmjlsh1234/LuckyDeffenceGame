using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Windows;

public class DataManager : SingletonBase<DataManager>
{
    #region ::::USERS
    public Users users = new Users();
    public UserSimple userSimple = new UserSimple();
    public AuthToken authToken = new AuthToken();
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

        LoadData("AuthToken", ref authToken);
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

    public void SaveData<T>(string fileName, T data)
    {
        string json = JsonConvert.SerializeObject(data);
        if (!System.IO.Directory.Exists(Constant.ABSOLUTE_FILE_PATH))
        {
            System.IO.Directory.CreateDirectory(Constant.ABSOLUTE_FILE_PATH);
        }

        string filePath = Constant.ABSOLUTE_FILE_PATH + fileName + ".json";

        // 파일에 JSON 쓰기
        System.IO.File.WriteAllText(filePath, json);
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
        

        //TODO : 등급 확률 체크
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
