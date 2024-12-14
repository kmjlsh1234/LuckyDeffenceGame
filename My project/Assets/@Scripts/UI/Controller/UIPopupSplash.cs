using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupSplash : UIBase
{
    [SerializeField] private GameObject loginButtonParent;
    [SerializeField] private TMP_InputField idField;
    [SerializeField] private TMP_InputField passField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button modUserInfoButton;
    [SerializeField] private Button googleLoginButton;

    

    public override void Init(ErrorCode code = ErrorCode.SUCCESS)
    {
        base.Init();
        AddEvent();
        
        if (DataManager.Instance.loginViewModel == null)
        {
            loginButtonParent.SetActive(true);
        }
        else
        {
            loginButtonParent.SetActive(false);
            LoginRequest();
        }
    }

    public void AddEvent()
    {
        loginButton.OnClickAsObservable().Subscribe(_ => LoginRequest()).AddTo(gameObject);
        joinButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Push(UIType.UIPopupJoin)).AddTo(gameObject);
        googleLoginButton.OnClickAsObservable().Subscribe(_ => LoginRequest()).AddTo(gameObject);
    }

    public void LoginRequest()
    {
        LoginViewModel loginForm;

        if(DataManager.Instance.loginViewModel != null)
        {
            loginForm = DataManager.Instance.loginViewModel;
        }
        else
        {
            loginForm = new LoginViewModel(idField.text, passField.text);
        }

        ConnectionManager.Instance.SendRequest(ServerURI.AUTH_LOGIN_REQUEST, loginForm, HTTP.POST, LoginResponse);
    }

    public void LoginResponse(UnityWebRequest res)
    {
        if (res.result == UnityWebRequest.Result.Success)
        {
            LoginSuccess(res);
        }
        else
        {
            LoginFail(res);
        }
    }

    private void LoginSuccess(UnityWebRequest res)
    {
        //jwt 저장

        //refresh토큰 저장

        //users 정보 저장

        //Main Scene으로 이동
        SceneManager.LoadScene(SceneName.MainScene.ToString());
    }

    private void LoginFail(UnityWebRequest res)
    {
        switch (res.error)
        {
            //유저 정보가 없다면 회원가입 화면 띄우기
            default:
                break;
        }

        //
    }
}
