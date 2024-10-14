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
        GameObject hero = PoolManager.Instance.Pop(randomHero, "hero", emptyPos.transform);

        hero.GetComponent<HeroBase>().Init(emptyPos);

        //TODO : 소환 처리
        emptyPos.IsEmpty = false;
        _generateButton.enabled = true;

        
    }
}
