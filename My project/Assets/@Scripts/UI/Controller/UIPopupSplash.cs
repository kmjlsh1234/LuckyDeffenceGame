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
    [SerializeField] private GameObject loginButtonParent;
    [SerializeField] private TMP_InputField idField;
    [SerializeField] private TMP_InputField passField;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button modUserInfoButton;
    [SerializeField] private Button googleLoginButton;

    private LoginViewModel loginViewModel;

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

        if(DataManager.Instance.loginViewModel != null)
        {
            loginViewModel = DataManager.Instance.loginViewModel;
        }
        else
        {
            loginViewModel = new LoginViewModel(idField.text, passField.text);
            Debug.Log($"ID : {idField.text}\nPW : {passField.text}");
        }

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
        UIManager.Instance.Push(UIType.UIPopupMessage);
    }
}
