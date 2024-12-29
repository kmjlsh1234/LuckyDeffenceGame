using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class ProfileController : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private Image profileImage;
    [SerializeField] private TMP_Text nickName;
    [SerializeField] private TMP_Text level;
    [SerializeField] private Slider exSlider;

    [SerializeField] private Button profileButton;

    public void Start()
    {
        //Event 등록
        AddEvent();

        //프로필 요청
        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_PROFILE_REQUEST, null, HTTP.GET, GetProfileResponse);
    }

    private void AddEvent()
    {
        profileButton.OnClickAsObservable().Subscribe(_ => UIManager.Instance.Push(UIType.UIPopupProfile)).AddTo(gameObject);
        DataManager.Instance.profile.Subscribe(profile =>
        {
            if (profile != null)
            {
                nickName.text = profile.nickname;                                           //nickname
                level.text = profile.level.ToString();                                      //level
                profileImage.sprite = ResourceManager.Instance.GetSprite(profile.image);    //image
                exSlider.value = profile.ex / profile.level * 100;                          //ex
            }

        }).AddTo(gameObject);
    }

    public void GetProfileResponse(UnityWebRequest res)
    {
        if (res.result == UnityWebRequest.Result.Success)
        {
            Profile profile = JsonConvert.DeserializeObject<Profile>(res.downloadHandler.text);
            if (profile.nickname != null)
            {
                DataManager.Instance.profile.Value = profile;
            }
            else
            {
                UIManager.Instance.Push(UIType.UIPopupModNickName);
            }
        }
    }
}
