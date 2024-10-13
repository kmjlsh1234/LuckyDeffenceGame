using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupSplash : UIBase
{
    [SerializeField] private Button _startButton;

    public override void Init()
    {
        base.Init();
        _startButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));
    }
}
