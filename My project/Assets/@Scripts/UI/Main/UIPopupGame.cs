using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class UIPopupGame : UIBase
{
    //Profile
    [SerializeField] private GameObject playerProfile;
    [SerializeField] private GameObject anotherPlayerProfile;
    [SerializeField] private Image playerProfileImage;
    [SerializeField] private Image anotherPlayerProfileImage;
    [SerializeField] private TMP_Text playerNickName;
    [SerializeField] private TMP_Text anotherPlayerNickName;
    [SerializeField] private TMP_Text playerLevel;
    [SerializeField] private TMP_Text anotherPlayerLevel;

    [SerializeField] private Button _moveMainSceneButton;
    [SerializeField] private Button _generateButton;

    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _currentEnemyCount;
    [SerializeField] private TMP_Text _currentMoney;
    [SerializeField] private TMP_Text _needGenerateMoney;

    public override void Init()
    {
        base.Init();
        GameManager.Instance.CurrentMoney = 40;
        AddEvent();
    }

    private void AddEvent()
    {
        GameManager.Instance.ObserveEveryValueChanged(x => x.WaveCount).Subscribe(x => _waveText.text = $"Wave {x.ToString()}");
        GameManager.Instance.ObserveEveryValueChanged(x => x.CurrentMoney).Subscribe(x => _currentMoney.text = x.ToString());
        GameManager.Instance.ObserveEveryValueChanged(x => x.NeedGenerateMoney).Subscribe(x => _needGenerateMoney.text = x.ToString());
        GameManager.Instance.ObserveEveryValueChanged(x => x.CheckGenerateValid()).Subscribe(x => 
        {
            _generateButton.enabled = x;
            _needGenerateMoney.color = x ? Color.white : Color.red;
        });
        GameManager.Instance.ObserveEveryValueChanged(x => x.TimerFloat)
            .Subscribe(x => 
            {
                int minutes = Mathf.FloorToInt(x / 60f);
                int seconds = Mathf.FloorToInt(x % 60f);

                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            });
        

        //���� �� ���� ������ ��ü
        PoolManager.Instance._enemyPool.ObserveEveryValueChanged(x => x.FindAll(x => x.activeInHierarchy).Count)
            .Subscribe(x => _currentEnemyCount.text = $"{x}/100");

        _moveMainSceneButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));
        _generateButton.onClick.AddListener(() => GenerateRandomHero());
    }

    private void GenerateRandomHero()
    {
        _generateButton.enabled = false;

        Pos emptyPos = MapManager.Instance.CurrentMap.FindEmptyPos();
        if(emptyPos == null)
        {
            Debug.LogWarning("No EmptyPos!");
            return;
        }

        //TODO : ��� ����
        GameManager.Instance.GenerateHero();

        //TODO : �������� ���� �����ϱ�
        string randomHero = DataManager.Instance.SelectRandomHeroByProbability();
        Debug.Log($"{randomHero} ��ȯ");

        //TODO : ������ ��ġ�� ���� ��ȯ�ϱ�
        GameObject hero = PoolManager.Instance.Pop(randomHero, "hero", emptyPos.transform);

        hero.GetComponent<HeroBase>().Init(emptyPos);

        //TODO : ��ȯ ó��
        emptyPos.isEmpty = false;
        _generateButton.enabled = true;
    }
}
