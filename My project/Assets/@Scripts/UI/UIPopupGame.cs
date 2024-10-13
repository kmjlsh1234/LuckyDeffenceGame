using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupGame : UIBase
{
    [SerializeField] private Button _moveMainSceneButton;
    [SerializeField] private Button _generateButton;
    public override void Init()
    {
        base.Init();
        _moveMainSceneButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));
        _generateButton.onClick.AddListener(() => GenerateRandomHero());
    }

    private void GenerateRandomHero()
    {
        _generateButton.enabled = false;

        //TODO : 소환할 돈이 있는지 확인하기

        Pos emptyPos = MapManager.Instance.CurrentMap.FindEmptyPos();
        if(emptyPos == null)
        {
            Debug.LogWarning("No EmptyPos!");
            return;
        }

        //TODO : 랜덤으로 영웅 선택하기
        string randomHero = DataManager.Instance.SelectRandomHeroByProbability();
        Debug.Log($"{randomHero} 소환");

        //TODO : 가져온 위치에 영웅 소환하기
        GameObject go = ResourceManager.Instance.GetHero(randomHero);
        GameObject hero = Instantiate(go);
        hero.transform.SetParent(emptyPos.transform);
        hero.transform.localPosition = Vector3.zero;
        hero.transform.localRotation = Quaternion.identity;
        hero.transform.localScale = Vector3.one;

        _generateButton.enabled = true;

        
    }
}
