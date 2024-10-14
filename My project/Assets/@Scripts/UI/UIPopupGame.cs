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

        //TODO : ��ȯ�� ���� �ִ��� Ȯ���ϱ�

        Pos emptyPos = MapManager.Instance.CurrentMap.FindEmptyPos();
        if(emptyPos == null)
        {
            Debug.LogWarning("No EmptyPos!");
            return;
        }
        
        //TODO : �������� ���� �����ϱ�
        string randomHero = DataManager.Instance.SelectRandomHeroByProbability();
        Debug.Log($"{randomHero} ��ȯ");

        //TODO : ������ ��ġ�� ���� ��ȯ�ϱ�
        GameObject hero = PoolManager.Instance.Pop(randomHero, "hero", emptyPos.transform);

        hero.GetComponent<HeroBase>().Init(emptyPos);

        //TODO : ��ȯ ó��
        emptyPos.IsEmpty = false;
        _generateButton.enabled = true;

        
    }
}
