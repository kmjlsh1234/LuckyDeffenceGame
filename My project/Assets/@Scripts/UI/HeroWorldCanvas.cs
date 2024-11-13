using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class HeroWorldCanvas : UIBase
{
    [SerializeField] private Button _summonButton;
    [SerializeField] private Button _sellButton;

    public void Start()
    {
        _summonButton.OnClickAsObservable().Subscribe(_ => Summon()).AddTo(gameObject);
        _sellButton.OnClickAsObservable().Subscribe(_ => Sell()).AddTo(gameObject);
    }

    private void Summon()
    {
        //TODO : 조합식 UI 띄우기
        HeroBase hero = transform.parent.GetComponent<HeroBase>();

        Debug.Log("Summon : " + hero.name);
    }

    private void Sell()
    {
        //TODO : 조합식 UI 띄우기
        Debug.Log("Sell");
    }
}
