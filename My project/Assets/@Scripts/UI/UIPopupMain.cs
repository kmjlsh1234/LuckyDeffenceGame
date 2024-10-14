using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enum;

public class UIPopupMain : UIBase
{
    [SerializeField] private Button _gameStartButton; 
    public override void Init()
    {
        base.Init();

        _gameStartButton.onClick.AddListener(() => {
            //���߿� �����Ѱŷ� �ٲٱ�
            MapManager.Instance.CurrentMapType = MapType.Map_City;
            SceneManager.LoadScene("GameScene");
        });

    }
}