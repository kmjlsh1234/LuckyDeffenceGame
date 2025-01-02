using Newtonsoft.Json;
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
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text energyTimer;

    private void Start()
    {
        //내부재화 조회
        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_GOLD_REQUEST, null, HTTP.GET, GetGoldResponse);

        //TODO : 외부재화 조회
        ConnectionManager.Instance.SendRequest<UnityWebRequest>(ServerURI.GET_DIAMOND_REQUEST, null, HTTP.GET, GetDiamondResponse);

        //TODO : 행동력 조회

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

    public void GetDiamondResponse(UnityWebRequest res)
    {
        if (res.result == UnityWebRequest.Result.Success)
        {
            string responseText = res.downloadHandler.text;
            if (int.TryParse(responseText, out int diamond))
            {
                diamondText.text = res.downloadHandler.text;
            }
        }
        else
        {
            UIManager.Instance.Push(UIType.UIPopupMessage);
        }
    }

    public void GetEnergyResponse(UnityWebRequest res)
    {
        if (res.result == UnityWebRequest.Result.Success)
        {
            string responseText = res.downloadHandler.text;
            EnergyVo energyVo = JsonConvert.DeserializeObject<EnergyVo>(responseText);
            if(energyVo != null)
            {
                energyText.text = energyVo.amount.ToString();
            }
        }
        else
        {
            UIManager.Instance.Push(UIType.UIPopupMessage);
        }
    }
}
