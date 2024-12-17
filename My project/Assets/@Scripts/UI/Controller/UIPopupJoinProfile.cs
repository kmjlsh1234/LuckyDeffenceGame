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

    public override void Init(ErrorCode code = ErrorCode.SUCCESS)
    {
        base.Init(code);
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
        switch (res.responseCode)
        {
            //OK
            case 200:
                //profile ¿˙¿Â
                UIManager.Instance.Pop();
                break;
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
