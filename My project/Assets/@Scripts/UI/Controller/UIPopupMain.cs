using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupMain : UIBase
{
    [SerializeField] private Button _gameStartButton; 
    public override void Init(ErrorCode code = ErrorCode.SUCCESS)
    {
        base.Init();

        _gameStartButton.onClick.AddListener(() => {
            //���߿� �����Ѱŷ� �ٲٱ�
            MapManager.Instance.CurrentMapType = MapType.Map_City;
            SceneManager.LoadScene("GameScene");
        });
    }
}
