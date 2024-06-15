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

        HealthTxt.text = "Health: " + dataInstance.userHealth.ToString();
        CurrentMoneyTxt.text = "Budget: " + dataInstance.currentMoney.ToString();
        DaysLeftTxt.text = "Days Left: " + dataInstance.daysLeft.ToString();
        GasTxt.text = "Gas: " + dataInstance.gasNum.ToString();
    }

    public void HideUI()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            GameUI.SetActive(false);
            ShopUI.SetActive(false);
        } else
        {
            GameUI.SetActive(false);
            DialoguePanel.SetActive(false);
        }
    }

    public void RestoreUI()
    {
        GameUI.SetActive(true);
    }

    public void OpenShopUI() { ShopUI.SetActive(true); HideUI(); }
    public void CloseShopUI() { ShopUI.SetActive(false); RestoreUI(); }
}
