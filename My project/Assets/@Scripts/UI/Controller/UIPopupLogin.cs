using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIPopupLogin : UIBase
{
    [SerializeField] private Button testLoginButton;
    [SerializeField] private Button googleLoginButton;
    [SerializeField] private Button customLoginButton;
    
    private LoginService loginService;

    private void Awake()
    {
        loginService = new LoginService();
    }

    public override void Init()
    {
        base.Init();
        AddEvent();
    }

    private void AddEvent()
    {
        testLoginButton.onClick.AsObservable().Subscribe(_ => loginService.TestLogin()).AddTo(gameObject);
        googleLoginButton.onClick.AsObservable().Subscribe(_ => loginService.GoogleLogin()).AddTo(gameObject);
        customLoginButton.onClick.AsObservable().Subscribe(_ => loginService.CustomLogin()).AddTo(gameObject);
    }
}
