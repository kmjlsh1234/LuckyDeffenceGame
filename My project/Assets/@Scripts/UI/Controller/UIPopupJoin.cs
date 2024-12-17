using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;
using UnityEngine.Networking;

public class UIPopupJoin : UIBase
{
    [SerializeField] private TMP_InputField idField;
    [SerializeField] private TMP_InputField passField;
    [SerializeField] private TMP_InputField passCheckField;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;
    public override void Init(ErrorCode code = ErrorCode.SUCCESS)
    {
        base.Init();
        AddEvent();
    }

    private void AddEvent()
    {
        joinButton.OnClickAsObservable().Subscribe(_ => OnClickJoinButton()).AddTo(gameObject);
        backButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Pop()).AddTo(gameObject);
    }

    public void OnClickJoinButton()
    {
        if(passCheckField.text != passCheckField.text)
        {
            passField.text = string.Empty;
            passCheckField.text = string.Empty;
            return;
        }

        UserJoinParam userJoinParam = new UserJoinParam(idField.text, passField.text);
        ConnectionManager.Instance.SendRequest(ServerURI.AUTH_JOIN_REQUEST, userJoinParam, HTTP.POST, JoinResponse);
    }

    public void JoinResponse(UnityWebRequest res)
    {
        switch (res.responseCode)
        {
            //OK
            case 200:
                UIManager.Instance.Pop();
                break;
            //No Content
            case 204: 
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
