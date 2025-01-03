using Newtonsoft.Json;
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
    [SerializeField] private Button autoLoginButton;
    private LoginViewModel loginViewModel;

    public override void Init()
    {
        base.Init();
        AddEvent();
    }

    public void AddEvent()
    {
        autoLoginButton.OnClickAsObservable().Subscribe(_ => AutoLogin()).AddTo(gameObject);
    }

    private void AutoLogin()
    {
        if (DataManager.Instance.loginViewModel == null)
        {
            UIManager.Instance.Push(UIType.UIPopupLogin);
        }
        else
        {
            LoginRequest();
        }
    }

    public void LoginRequest()
    {
        loginViewModel = DataManager.Instance.loginViewModel;
        ConnectionManager.Instance.SendRequest(ServerURI.AUTH_LOGIN_REQUEST, loginViewModel, HTTP.POST, LoginResponse);
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
        //jwt 조회
        string jwtToken = res.GetResponseHeader("Authorization");

        //refresh토큰 조회
        string refreshToken = res.GetResponseHeader("ReAuthentication");
        AuthToken authToken = new AuthToken(jwtToken, refreshToken);
        DataManager.Instance.authToken = authToken;

        //users 정보 저장
        string jsonResponse = res.downloadHandler.text;
        UserSimple userSimple = JsonConvert.DeserializeObject<UserSimple>(jsonResponse);
        DataManager.Instance.userSimple = userSimple;

        //로컬 데이터 저장
        DataManager.Instance.SaveData("LoginViewModel", loginViewModel);
        DataManager.Instance.SaveData("AuthToken", authToken);

        //Main Scene으로 이동
        SceneManager.LoadScene(SceneName.MainScene.ToString());
    }

    private void LoginFail(UnityWebRequest res)
    {
        UIManager.Instance.Push(UIType.UIPopupLogin);
    }
}