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
        if (res.result == UnityWebRequest.Result.Success)
        {
            Debug.LogError("BreakPoint");
            UIManager.Instance.Pop();
        }
        else
        {

        }
    }
}