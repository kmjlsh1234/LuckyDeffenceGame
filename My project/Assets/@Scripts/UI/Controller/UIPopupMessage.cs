using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class UIPopupMessage : UIBase
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button backButton;

    public override void Init(ErrorCode code = ErrorCode.SUCCESS)
    {
        base.Init();

        messageText.text = code.ToString();

        backButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Pop()).AddTo(gameObject);
    }
}
