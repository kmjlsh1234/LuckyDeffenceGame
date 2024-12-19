using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainScene : SceneBase
{
    
    public override void Init()
    {
        base.Init();
        _sceneName = SceneName.MainScene;
        UIManager.Instance.Push(UIType.UIPopupMain);

        //프로필 요청
        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_PROFILE_REQUEST, null, HTTP.GET, GetProfileResponse);
    }

    public void GetProfileResponse(UnityWebRequest res)
    {
        //성공 시 
        Profile profile = JsonConvert.DeserializeObject<Profile>(res.downloadHandler.text);
        if (profile != null)
        {
            DataManager.Instance.profile = profile;
            UIManager.Instance.Pop();
        }        
    }
}
