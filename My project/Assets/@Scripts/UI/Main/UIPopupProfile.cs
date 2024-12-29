using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupProfile : UIBase
{
    [SerializeField] private Image profileImage;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Slider exSlider;
    [SerializeField] private TMP_Text exp;

    [SerializeField] private Button modNickNameButton;
    [SerializeField] private Button backButton;
    public override void Init()
    {
        base.Init();

        AddEvent();
    }

    private void AddEvent()
    {
        modNickNameButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Push(UIType.UIPopupModNickName)).AddTo(gameObject);
        backButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Pop()).AddTo(gameObject);

        DataManager.Instance.profile.Subscribe(profile =>
        {
            if (profile != null)
            {
                nickname.text = profile.nickname;                                           //nickname
                level.text = profile.level.ToString();                                      //level
                profileImage.sprite = ResourceManager.Instance.GetSprite(profile.image);    //image
                exSlider.value = profile.ex / profile.level * 100;                          //ex
                exp.text = $"{profile.ex}/{profile.level * 100}";                           //exText
            }

        }).AddTo(gameObject);
    }
}
