using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupMain : UIBase
{
    [Header("Profile")]
    [SerializeField] private Image profileImage;
    [SerializeField] private TMP_Text nickName;
    [SerializeField] private TMP_Text level;

    [Space(10f)]

    [SerializeField] private Button gameStartButton;
    
    
    public override void Init()
    {
        base.Init();
        
        AddEvent();

        //������ ��û
        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_PROFILE_REQUEST, null, HTTP.GET, GetProfileResponse);
    }

    private void AddEvent()
    {
        gameStartButton.onClick.AddListener(() => OnClickGameStartButton());
        //Profile
        DataManager.Instance.profile.Subscribe(profile =>
        {
            if (profile != null)
            {
                nickName.text = profile.nickname;
                level.text = profile.level.ToString();
            }
            
        }).AddTo(gameObject);
    }

    private void OnClickGameStartButton()
    {
        //���߿� �����Ѱŷ� �ٲٱ�
        MapManager.Instance.CurrentMapType = MapType.Map_City;
        SceneManager.LoadScene(SceneName.GameScene.ToString());
    }

    public void SetProfileData(Profile profile)
    {
        nickName.text = profile.nickname;
        level.text = profile.level.ToString();
        //TODO : �̹��� ResourcesManager���� ������ ����
    }

    public void GetProfileResponse(UnityWebRequest res)
    {
        if (res.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(res.downloadHandler.text);
            Profile profile = JsonConvert.DeserializeObject<Profile>(res.downloadHandler.text);
            if (profile != null)
            {
                DataManager.Instance.profile.Value = profile;
                return;
            }
        }
        UIManager.Instance.Push(UIType.UIPopupJoin);
    }
}
