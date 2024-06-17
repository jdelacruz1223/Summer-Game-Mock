using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSetupManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameTxt;
    [SerializeField] private TMP_InputField partyTxt;
    [SerializeField] private TextMeshProUGUI partyTextFormat;
    [SerializeField] private GameObject partyListPanel;
    public string username { get; private set; }
    public string partyName { get; private set; }
    public int totalMembers { get; private set; }

    List<string> partyNames;

    // Start is called before the first frame update
    void Start()
    {
        partyNames = new List<string>();
    }

    IEnumerator RefreshPartyList()
    {

        float distBetweenText = 15f;
        int index = 0;

        foreach (Transform child in partyListPanel.transform)
        {
            
            if (child.gameObject.name != "TextFormat")
                Destroy(child.gameObject);
        }

        yield return new WaitForEndOfFrame();

        foreach (var name in partyNames)
        {
            Debug.Log(name);

            TextMeshProUGUI text = Instantiate(partyTextFormat, partyListPanel.transform);
            text.gameObject.SetActive(true);

            text.text = name;

            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -distBetweenText * index);

            index++;
        }
    }
    public void AddToParty()
    {
        if (partyNames.Contains(partyName)) return;
        if (partyTxt.text.Length == 0) return;

        Manager.GetInstance().addToParty(partyName);
        partyNames.Add(partyName);
        StartCoroutine(RefreshPartyList());

        partyTxt.text = "";
    }
    public void RemoveFromParty(int x) {
        partyNames.RemoveAt(x);
        TextMeshProUGUI[] child = partyListPanel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var c in child) Destroy(c);

        StartCoroutine(RefreshPartyList());
        Manager.GetInstance().removeToParty(name);
    }

    public void FinishCharacterSetup()
    {
        Manager.GetInstance().setUsername(username);

        SceneManager.LoadScene("TravelScene");
        Destroy(this.gameObject);
    }

    public void setName() => username = nameTxt.text;
    public void setPartyName() => partyName = partyTxt.text;
}
