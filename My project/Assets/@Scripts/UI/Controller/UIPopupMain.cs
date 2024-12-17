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

        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_PROFILE_REQUEST, null, HTTP.GET, GetProfileResponse);

    }

    public void GetProfileResponse(UnityWebRequest res)
    {
        //Profile Not Found�� Profile �Է� ȭ�� ����
    }
}
