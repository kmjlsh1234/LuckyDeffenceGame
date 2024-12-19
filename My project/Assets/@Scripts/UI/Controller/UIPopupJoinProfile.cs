using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIPopupJoinProfile : UIBase
{
    [SerializeField] private TMP_InputField nickNameField;
    [SerializeField] private Button saveButton;

    public override void Init()
    {
        base.Init();
        saveButton.OnClickAsObservable().Subscribe(_ => JoinProfileRequest()).AddTo(gameObject);
    }

    public void JoinProfileRequest()
    {
        if(nickNameField.text.Length > 15 || nickNameField.text == string.Empty)
        {
            
        }
        ProfileAddParam profileAddParam = new ProfileAddParam(nickNameField.text);
        ConnectionManager.Instance.SendRequest(ServerURI.ADD_PROFILE_REQUEST, profileAddParam, HTTP.POST, JoinProfileResponse);
    }

    public void JoinProfileResponse(UnityWebRequest res)
    {
        if(res.result == UnityWebRequest.Result.Success)
        {
            Profile profile = JsonConvert.DeserializeObject<Profile>(res.downloadHandler.text);
            
            if(profile != null)
            {
                DataManager.Instance.profile = profile;
            }
            else
            {
                Debug.LogError("Profile Deserialize Fail");
            }

            UIManager.Instance.Pop();
        }
    }
    
}
