using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager me;

    public int Money { get; private set; }
    public int Food { get; private set; }
    public int Tires { get; private set; }
    public int Entertainment { get; private set; }

    public Text moneyText;
    public Text foodText;
    public Text tiresText;
    public Text entertainmentText;

    void Awake()
    {
        if (me != null) // If an instance of this class already exists
        {
            Destroy(gameObject);  // Destroy this new instance
            return; // Exit the function
        }

        me = this;  // Store this instance in a static variable
        DontDestroyOnLoad(gameObject);  // Do not destroy this object when the current scene ends and a new one begins
    }

    void Start()
    {
        // Initialize resource values if needed
        SetInitialValues(500, 100, 4, 50); // Example initial values
        UpdateUI();  // Ensure UI is updated with initial positions
    }

    public void UpdateMoney(int amount)
    {
        Money += amount;
        UpdateUI();
    }

    public void UpdateFood(int amount)
    {
        Food += amount;
        UpdateUI();
    }

    public void UpdateTires(int amount)
    {
        Tires += amount;
        UpdateUI();
    }

    public void UpdateEntertainment(int amount)
    {
        Entertainment += amount;
        UpdateUI();
    }

    public void SetInitialValues(int money, int food, int tires, int entertainment)
    {
        Money = money;
        Food = food;
        Tires = tires;
        Entertainment = entertainment;
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Convert integers to strings and update the text components
        moneyText.text = "Money: " + Money.ToString();
        foodText.text = "Food: " + Food.ToString();
        tiresText.text = "Tires: " + Tires.ToString();
        entertainmentText.text = "Entertainment: " + Entertainment.ToString();
    }
}
