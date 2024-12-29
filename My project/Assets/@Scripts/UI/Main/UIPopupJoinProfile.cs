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
        ProfileModParam profileModParam = new ProfileModParam(nickNameField.text, null);
        ConnectionManager.Instance.SendRequest(ServerURI.ADD_PROFILE_REQUEST, profileModParam, HTTP.PUT, JoinProfileResponse);
    }

    public void JoinProfileResponse(UnityWebRequest res)
    {
        if(res.result == UnityWebRequest.Result.Success)
        {
            Profile profile = JsonConvert.DeserializeObject<Profile>(res.downloadHandler.text);
            
            if(profile != null)
            {
                DataManager.Instance.profile.Value = profile;
                UIManager.Instance.Pop();
            }
            else
            {
                Debug.LogError("Profile Deserialize Fail");
            }
        }
    }
    
}
