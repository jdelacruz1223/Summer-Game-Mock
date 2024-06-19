using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject GameUI;
    [SerializeField] private GameObject ShopUI;
    [SerializeField] private GameObject DialoguePanel;

    [Header("Item UI")]
    public TextMeshProUGUI DriverTxt;
    public TextMeshProUGUI HealthTxt;
    public TextMeshProUGUI CurrentMoneyTxt;
    public TextMeshProUGUI DaysLeftTxt;
    public TextMeshProUGUI GasTxt;
    public TextMeshProUGUI DistanceTxt;

    private static UIManager instance;
    public static UIManager GetInstance() { return instance; }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Update()
    {
        StartCoroutine(updateUI());
    }

    IEnumerator updateUI()
    {
        yield return new WaitForEndOfFrame();
        var dataInstance = Manager.GetInstance();

        DriverTxt.text = dataInstance.username;
        //HealthTxt.text = dataInstance.userHealth.ToString();
        CurrentMoneyTxt.text = dataInstance.currentMoney.ToString();
        DaysLeftTxt.text = dataInstance.daysLeft.ToString();
        GasTxt.text = dataInstance.gasNum.ToString();
        //DistanceTxt.text = dataInstance.currentProgress.ToString() + "%";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">The UI Game Object</param>
    /// <param name="hide">True for Hide, False for Show</param>
    public void ControlUI(GameObject obj, bool show)
    {
        if (obj != null) obj.SetActive(show);
        else Debug.Log("Panel is not set. Ignored.");
    }

    public void HideUI()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            ControlUI(ShopUI, false);
            ControlUI(GameUI, false);
        } else
        {
            ControlUI(GameUI, false);
            ControlUI(DialoguePanel, false);
        }
    }

    public void RestoreUI()
    {
        ControlUI(GameUI, true);
    }

    public void OpenShopUI() { ControlUI(ShopUI, true); HideUI(); }
    public void CloseShopUI() { ControlUI(ShopUI, false); RestoreUI(); }
}
