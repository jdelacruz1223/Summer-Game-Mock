using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSetupManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameTxt;
    [SerializeField] private TMP_InputField partyTxt;
    [SerializeField] private TextMeshProUGUI partyTextFormat;
    [SerializeField] private GameObject partyListPanel;
    public List<GameObject> playersInGame;

    public List<Sprite> playerSprites;
    public List<AnimatorOverrideController> animationForPlayerSprites;
    public string username { get; private set; }
    public string partyName { get; private set; }
    public int totalMembers { get; private set; }

    List<string> partyNames;

    // Start is called before the first frame update
    void Start()
    {
        partyNames = new List<string>();

        RandomSprite();
    }

    void RandomSprite()
    {
        if (playerSprites.Count == 0)
        {
            Debug.LogError("No more sprites available.");
            return;
        }

        int index = Random.Range(0, playerSprites.Count);
        var sprite = playerSprites[index];

        if (Manager.GetInstance().playerSprite.sprite == null)
        {
            Debug.Log("Setting sprite for player");
            Debug.Log(sprite);
            Manager.GetInstance().setPlayerSprite(new Assets.Model.SpriteModel
            {
                sprite = sprite,
                animator = animationForPlayerSprites[index]
            }) ;

            playersInGame[0].GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            Debug.Log("Adding new sprite");

            Manager.GetInstance().addPartySprite(sprite);

            var player = playersInGame[0];
            Debug.Log("Setting sprite for player " + player.gameObject.name);

            if (player.gameObject.name != "Player")
            {
                Debug.Log("Setting sprite for party member");
                player.SetActive(true);
                var spriteRender = player.GetComponentInChildren<SpriteRenderer>();
                spriteRender.sprite = sprite;
            }
        }

        playersInGame.RemoveAt(0);
        playerSprites.RemoveAt(index);
        animationForPlayerSprites.RemoveAt(index);
    }

    IEnumerator RefreshPartyList()
    {
        float distBetweenText = 10f;
        int index = 0;

        foreach (Transform child in partyListPanel.transform)
        {
            
            if (child.gameObject.name != "TextFormat")
                Destroy(child.gameObject);
        }

        yield return new WaitForEndOfFrame();

        foreach (var name in partyNames)
        {
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
        if (partyNames.Count == 3) return;
        if (partyNames.Contains(partyName)) return;
        if (partyTxt.text.Length == 0) return;

        Manager.GetInstance().addToParty(partyName);
        partyNames.Add(partyName);

        RandomSprite();
        
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
        SceneManager.LoadScene("TravelScene");
        Destroy(this.gameObject);
    }

    public void setName() => Manager.GetInstance().setUsername(nameTxt.text);
    public void setPartyName() => partyName = partyTxt.text;
}
