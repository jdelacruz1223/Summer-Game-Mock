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

    [Header("Supplies Index UI")]
    public TextMeshProUGUI tiresIndexTxt;
    public TextMeshProUGUI foodIndexTxt;
    public TextMeshProUGUI fishbaitIndexTxt;
    public TextMeshProUGUI medicineIndexTxt;


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
        CurrentMoneyTxt.text = dataInstance.currentMoney.ToString();
        GasTxt.text = dataInstance.gasNum.ToString();

        tiresIndexTxt.text = dataInstance.tiresNum.ToString();
        foodIndexTxt.text = dataInstance.snacksNum.ToString();
        fishbaitIndexTxt.text = dataInstance.fishbaitNum.ToString();
        medicineIndexTxt.text = dataInstance.medicineNum.ToString();
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
