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
        switch (res.responseCode)
        {
            //OK
            case 200:
                Profile profile = JsonConvert.DeserializeObject<Profile>(res.downloadHandler.text);
                if(profile != null)
                {
                    DataManager.Instance.profile = profile;
                    UIManager.Instance.Pop();
                }
                break;
            //Bad Request
            case 400:
                UIManager.Instance.Push(UIType.UIPopupMessage, ErrorCode.USER_NOT_EXIST);
                break;
            //Unauthorized(need token)
            case 401:
                UIManager.Instance.Push(UIType.UIPopupMessage, ErrorCode.USER_NOT_EXIST);
                break;
            //Forbidden(auth Error)
            case 403:
                UIManager.Instance.Push(UIType.UIPopupMessage, ErrorCode.USER_NOT_EXIST);
                break;
            //Not Found
            case 404:
                UIManager.Instance.Push(UIType.UIPopupMessage, ErrorCode.USER_NOT_EXIST);
                break;
            //Internal Server Error
            case 500:
                UIManager.Instance.Push(UIType.UIPopupMessage, ErrorCode.USER_NOT_EXIST);
                break;
        }
    }

}
