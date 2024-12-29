using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CurrencyController : MonoBehaviour
{
    [Header("Currency")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text diamondText;

    private void Start()
    {
        //내부재화 요청
        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_GOLD_REQUEST, null, HTTP.GET, GetGoldResponse);
        //TODO : 외부재화 요청
    }

    public void GetGoldResponse(UnityWebRequest res)
    {
        if (res.result == UnityWebRequest.Result.Success)
        {
            string responseText = res.downloadHandler.text;
            if (int.TryParse(responseText, out int gold))
            {
                goldText.text = res.downloadHandler.text;
            }
        }
        else
        {
            UIManager.Instance.Push(UIType.UIPopupMessage);
        }
    }
}
