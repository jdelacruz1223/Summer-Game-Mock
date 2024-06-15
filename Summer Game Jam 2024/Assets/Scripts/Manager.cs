using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /// <summary>
    /// Intro Setup Variables
    /// </summary>
    public string username { get; private set; }
    public List<string> party { get; private set; }
    public int budget { get; private set; }
    [SerializeField] public int currentMoney { get; private set; }

    /// <summary>
    /// All In Game Item Variables
    /// </summary>
    [SerializeField] public int tiresNum { get; private set; }
    [SerializeField] public int snacksNum { get; private set; }
    [SerializeField] public int booksNum { get; private set; }
    [SerializeField] public int gameNum { get; private set; }
    [SerializeField] public int drugsNum { get; private set; }

    /// <summary>
    /// Gameplay Variables
    /// </summary>
    public int daysLeft { get; private set; }
    [SerializeField] public float gasNum { get; private set; }
    [SerializeField] public float userHealth { get; private set; }

    /// <summary>
    /// End Game Scene Variables
    /// </summary>
    public float totalTime { get; private set; }
    public float homeToSolvangTime { get; private set; }
    public float solvangToPismoTime { get; private set; }
    public float pismoToMontereyTime { get; private set; }
    public float montereyToSFTime { get; private set; }
    public int relicsNum { get; private set; }

    public static Manager GetInstance() { return me; }

    public static Manager me;
    void Awake()
    {
        if (me != null) 
        {
            Destroy(gameObject);
            return;
        }

        me = this;  
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        party = new List<string>();

        budget = 500;
        currentMoney = budget;

        tiresNum = 0;
        snacksNum = 0;
        booksNum = 0;
        gameNum = 0;
        drugsNum = 0;

        daysLeft = 5;
        gasNum = 100;
        userHealth = 100;
    }

    int lessCheck(int initial, int new_value) { int val = initial - new_value; if (val >= 0) return val; else return 0; }

    #region INTRO SETUP
    public void setUsername(string name) => username = name;
    public bool addToParty(string name) { if (!party.Contains(name)) { party.Add(name); return true; } else return false; }
    public bool removeToParty(string name) => party.Remove(name);

    public void setBudget(int value) => budget = value;
    public void increaseMoneyCount(int value) { currentMoney += value; }
    public void decreaseMoneyCount(int value) => currentMoney = lessCheck(currentMoney, value);
    #endregion

    #region IN GAME ITEMS
    /// <summary>
    /// All the Gameplay Items
    /// </summary>
    /// <param name="value"></param>
    public void increaseTireCount(int value) => tiresNum += value;
    public void decreaseTireCount(int value) => tiresNum = lessCheck(tiresNum, value);

    public void increaseSnackCount(int value) => snacksNum += value;
    public void decreaseSnackCount(int value) => snacksNum = lessCheck(snacksNum, value);

    public void increaseBookCount(int value) => booksNum += value;
    public void decreaseBookCount(int value) => booksNum = lessCheck(booksNum, value);

    public void increaseGameCount(int value) => gameNum += value;
    public void decreaseGameCount(int value) => gameNum = lessCheck(gameNum, value);

    public void increaseDrugsCount(int value) => drugsNum += value;
    public void decreaseDrugsCount(int value) => drugsNum = lessCheck(drugsNum, value);
    #endregion

    #region END GAME SCENE
    public void decreaseDaysLeftCount() => daysLeft -= 1;

    public void increaseGasCount(float value) => gasNum += value;
    public void decreaseGasCount(float value)
    {
        float val = gasNum - value;
        if (val >= 0) gasNum = val; else gasNum = 0; 
    }

    public void increaseUserHealth(float value) => userHealth += value;
    public void decreaseUserHealth(float value)
    {
        float val = userHealth - value;
        Debug.Log(val);
        if (val >= 0) userHealth = val; else userHealth = 0;
    }
    #endregion
}
