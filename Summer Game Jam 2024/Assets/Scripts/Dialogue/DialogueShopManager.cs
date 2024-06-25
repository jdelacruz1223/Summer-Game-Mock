using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ShopManager;

public class DialogueShopManager : MonoBehaviour
{
    private static DialogueShopManager instance;
    public static DialogueShopManager GetInstance() { return instance; }
    [Header("Shop UI")]
    public GameObject shopPanel;
    public TextMeshProUGUI currentMoneyTxt;
    public TextMeshProUGUI tiresIndex;
    public TextMeshProUGUI foodIndex;
    public TextMeshProUGUI baitIndex;
    public TextMeshProUGUI medkitIndex;

    private void Awake()
    {
        instance = this;
    }

    public void OpenShop()
    {
        var manager = Manager.GetInstance();
        DialogueManager.GetInstance().dialogueIsPlaying = true;

        shopPanel.SetActive(true);

        currentMoneyTxt.text = manager.currentMoney.ToString();
        tiresIndex.text = manager.tiresNum.ToString();
        foodIndex.text = manager.snacksNum.ToString();
        baitIndex.text = manager.fishbaitNum.ToString();
        medkitIndex.text = manager.medicineNum.ToString();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        DialogueManager.GetInstance().dialogueIsPlaying = false;
    }

    public void MakeChoice(int item)
    {
        var manager = Manager.GetInstance();

        Items selectedItem = (Items)item;
        var priceOfItem = Manager.GetInstance().PriceOfItem(selectedItem.ToString());
        if (!Manager.GetInstance().CanAfford(priceOfItem)) return;

        manager.decreaseMoneyCount(priceOfItem);
        switch (selectedItem)
        {
            case Items.Tires:
                // Buy Tires
                manager.increaseTireCount(1);
                tiresIndex.text = manager.tiresNum.ToString();
                break;
            case Items.Food:
                // Buy Food
                manager.increaseSnackCount(1);
                foodIndex.text = manager.snacksNum.ToString();
                break;
            case Items.Fish_Bait:
                // Buy Fish bait
                manager.increaseFishbaitCount(1);
                baitIndex.text = manager.fishbaitNum.ToString();
                break;
            case Items.Medicine:
                // Buy Medicine
                manager.increaseMedicineCount(1);
                medkitIndex.text = manager.medicineNum.ToString();
                break;
        }

        currentMoneyTxt.text = manager.currentMoney.ToString();
    }
}
