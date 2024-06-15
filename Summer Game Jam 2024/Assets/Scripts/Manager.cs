using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    /// <summary>
    /// Intro Setup Variables
    /// </summary>
    public string username { get; private set; }
    public List<string> party { get; private set; }
    public int budget { get; private set; }
    public int currentMoney { get; private set; }

    /// <summary>
    /// All In Game Item Variables
    /// </summary>
    public int tiresNum { get; private set; }
    public int snacksNum { get; private set; }
    public int booksNum { get; private set; }
    public int gameNum { get; private set; }
    public int drugsNum { get; private set; }

    /// <summary>
    /// Gameplay Variables
    /// </summary>
    public int daysLeft { get; private set; }
    public float gasNum { get; private set; }
    public float userHealth { get; private set; }

    /// <summary>
    /// End Game Scene Variables
    /// </summary>
    public float totalTime { get; private set; }

    public static Manager GetInstance() { return me; }

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

    }

    int lessCheck(int initial, int new_value) { int val = initial - new_value; if (val >= 0) return val; else return 0; }

    #region Intro Setup
    public void setUsername(string name) => username = name;
    public bool addToParty(string name) { if (!party.Contains(name)) { party.Add(name); return true; } else return false; }
    public void removeToParty(string name) => party.Remove(name);

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

}
