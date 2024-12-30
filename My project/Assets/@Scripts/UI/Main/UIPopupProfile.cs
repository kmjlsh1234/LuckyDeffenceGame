using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIPopupProfile : UIBase
{
    #region ::::Property
    [Header("profile")]
    [SerializeField] private Image profileImage;
    [SerializeField] private TMP_Text nickname;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Slider exSlider;
    [SerializeField] private TMP_Text exp;

    [Space(10f)]

    [Header("stats")]
    [SerializeField] private TMP_Text longestPlayTime;
    [SerializeField] private TMP_Text highestWaveCount;
    [SerializeField] private TMP_Text bossKillCount;
    [SerializeField] private TMP_Text killCount;
    [SerializeField] private TMP_Text totalPlayTime;
    [SerializeField] private TMP_Text soloPlayTime;
    [SerializeField] private TMP_Text multiPlayTime;
    [SerializeField] private TMP_Text totalPlayCount;
    [SerializeField] private TMP_Text soloPlayCount;
    [SerializeField] private TMP_Text multiPlayCount;

    [Space(10f)]

    [Header("button")]
    [SerializeField] private Button modNickNameButton;
    [SerializeField] private Button backButton;
    #endregion

    public override void Init()
    {
        base.Init();
        AddEvent();

        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_STAT_REQUEST, null, HTTP.GET, GetStatResponse);
    }

    private void AddEvent()
    {
        //BUTTON_Binding
        modNickNameButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Push(UIType.UIPopupModNickName)).AddTo(gameObject);
        backButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Pop()).AddTo(gameObject);

        //DATA Binding
        DataManager.Instance.profile.Subscribe(profile => SetProfile(profile)).AddTo(gameObject);
        DataManager.Instance.stat.Subscribe(stat => SetStat(stat)).AddTo(gameObject);
    }

    private void SetProfile(Profile profile)
    {
        if (profile != null)
        {
            nickname.text = profile.nickname;                                           
            level.text = profile.level.ToString();                                      
            profileImage.sprite = ResourceManager.Instance.GetSprite(profile.image);    
            exSlider.value = profile.ex / profile.level * 100;                         
            exp.text = $"{profile.ex}/{profile.level * 100}";                          
        }
    }

    private void SetStat(Stat stat)
    {
        if(stat != null)
        {
            longestPlayTime.text = stat.longestPlayTime.ToString();
            bossKillCount.text = stat.bossKillCount.ToString();
            killCount.text = stat.killCount.ToString();
            totalPlayTime.text = (stat.soloPlayTime + stat.multiPlayTime).ToString();
            soloPlayTime.text = stat.soloPlayTime.ToString();
            multiPlayTime.text = stat.multiPlayTime.ToString();
            totalPlayCount.text = (stat.soloPlayCount + stat.multiPlayCount).ToString();
            soloPlayCount.text = stat.soloPlayCount.ToString();
            multiPlayCount.text= stat.multiPlayCount.ToString();
        }
    }

    public void GetStatResponse(UnityWebRequest res)
    {
        if(res.result == UnityWebRequest.Result.Success)
        {
            Stat stat = JsonConvert.DeserializeObject<Stat>(res.downloadHandler.text);
            DataManager.Instance.stat.Value = stat;
        }
        else
        {
            UIManager.Instance.Push(UIType.UIPopupMessage);
        }
    }
}
