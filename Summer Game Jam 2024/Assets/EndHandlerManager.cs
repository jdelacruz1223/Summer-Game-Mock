using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndHandlerManager : MonoBehaviour
{
    public TextMeshProUGUI totalTimeTxt;
    public TextMeshProUGUI moneySavedTxt;
    public TextMeshProUGUI fishCaughtTxt;
    public TextMeshProUGUI encountersTxt;

    void Start()
    {
        var instance = Manager.GetInstance();
        totalTimeTxt.text = "";
        moneySavedTxt.text = instance.currentMoney.ToString();
        fishCaughtTxt.text = instance.fishCaughtNum.ToString();
        encountersTxt.text = instance.encountersNum.ToString();
    }
}
