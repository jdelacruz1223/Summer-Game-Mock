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

        float currentTime = instance.GetTotalTimeElapsed();
        float finalTime = 0f;

        if (currentTime >= 60)
        {
            finalTime = currentTime / 60;
            finalTime = Mathf.Round(finalTime);
        }
        totalTimeTxt.text = finalTime.ToString();
        if (currentTime > 60) totalTimeTxt.text += " minutes";

        moneySavedTxt.text = "$" + instance.currentMoney.ToString();
        fishCaughtTxt.text = instance.fishCaughtNum.ToString();
        encountersTxt.text = instance.encountersNum.ToString();
    }
}
