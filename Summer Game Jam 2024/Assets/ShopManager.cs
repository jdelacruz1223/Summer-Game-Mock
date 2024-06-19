using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    List<ItemModel> itemsInShop;
    public TextMeshProUGUI budgetTxt;
    public int totalSpent;
    public int currentBudget;

    [Header("Indexes")]
    public TextMeshProUGUI TiresIndex;
    public TextMeshProUGUI FoodIndex;
    public TextMeshProUGUI FishBaitIndex;
    public TextMeshProUGUI MedicineIndex;

    private int tireIndex;
    private int foodIndex;
    private int fishbaitIndex;
    private int medicineIndex;

    public enum Items
    {
        Tires,
        Food,
        Fish_Bait,
        Medicine
    }

    void Start()
    {
        itemsInShop = new List<ItemModel>
        {
            new ItemModel { Name = "Tires", Price = 100 },
            new ItemModel { Name = "Food", Price = 15 },
            new ItemModel { Name = "Fish_Bait", Price = 5 },
            new ItemModel { Name = "Medicine", Price = 30 }
        };

        budgetTxt.text = Manager.GetInstance().currentMoney.ToString();
        currentBudget = Manager.GetInstance().currentMoney;

        TiresIndex.text = "1x";
        FoodIndex.text = "1x";
        FishBaitIndex.text = "1x";
        MedicineIndex.text = "1x";

        tireIndex = 1;
        foodIndex = 1;
        fishbaitIndex = 1;
        medicineIndex = 1;

        totalSpent = 0;
    }

    bool CanAfford(int value)
    {
        var final_value = currentBudget - value;
        
        if (final_value == 0) return true;
        if (final_value > 0) return true;
        return false;
    }

    int PriceOfItem(string item) {
        return itemsInShop.FirstOrDefault(i => i.Name == item).Price;
    }

    void UpdateUI()
    {
        budgetTxt.text = currentBudget.ToString();
        TiresIndex.text = $"{tireIndex}x";
        FoodIndex.text = $"{foodIndex}x";
        FishBaitIndex.text = $"{fishbaitIndex}x";
        MedicineIndex.text = $"{medicineIndex}x";
    }

    public void AddToCart(int item)
    {
        print(item);
        Items selectedItem = (Items)item;
        print(selectedItem);
        var priceOfItem = PriceOfItem(selectedItem.ToString());
        if (!CanAfford(priceOfItem)) return;

        switch (selectedItem)
        {
            case Items.Tires:
                if (tireIndex < 10)
                    tireIndex++;
                    totalSpent += priceOfItem;
                    currentBudget -= priceOfItem;
                break;
            case Items.Food:
                if (foodIndex < 10)
                    foodIndex++;
                    totalSpent += priceOfItem;
                    currentBudget -= priceOfItem;
                break;
            case Items.Fish_Bait:
                if (fishbaitIndex < 10)
                    fishbaitIndex++;
                    totalSpent += priceOfItem;
                    currentBudget -= priceOfItem;
                break;
            case Items.Medicine:
                if (medicineIndex < 10)
                    medicineIndex++;
                    totalSpent += priceOfItem;
                    currentBudget -= priceOfItem;
                break;
        }

        UpdateUI();
    }

    public void DecreaseCount(int item)
    {
        Items selectedItem = (Items)item;
        var priceOfItem = PriceOfItem(selectedItem.ToString());

        switch (selectedItem)
        {
            case Items.Tires:
                if (tireIndex == 0) return;

                if (tireIndex > 0)
                    tireIndex--;
                    totalSpent -= priceOfItem;
                    currentBudget += priceOfItem;
                break;
            case Items.Food:
                if (foodIndex == 0) return;

                if (foodIndex > 0)
                    foodIndex--;
                    totalSpent -= priceOfItem;
                    currentBudget += priceOfItem;
                break;
            case Items.Fish_Bait:
                if (fishbaitIndex == 0) return;

                if (fishbaitIndex > 0)
                    fishbaitIndex--;
                    totalSpent -= priceOfItem;
                    currentBudget += priceOfItem;
                break;
            case Items.Medicine:
                if (medicineIndex == 0) return;

                if (medicineIndex > 0)
                    medicineIndex--;
                    totalSpent -= priceOfItem;
                    currentBudget += priceOfItem;
                break;
        }

        UpdateUI();
    }
}
