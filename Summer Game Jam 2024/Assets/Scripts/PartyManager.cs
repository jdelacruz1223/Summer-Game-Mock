using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    [Header("Party Members")]
    public GameObject partyPanel;
    public GameObject playerPanel;
    List<GameObject> playerPanelSequence;
    public float distanceBetweenMembers = 5f;

    void Start()
    {
        List<PartyModel> partylist = Manager.GetInstance().party;
        playerPanelSequence = new List<GameObject>();

        int index = 1;
        foreach (PartyModel party in partylist)
        {
            var obj = Instantiate(playerPanel, partyPanel.transform);
            obj.SetActive(true);

            var text = obj.GetComponentInChildren<TextMeshProUGUI>();
            var slider = obj.GetComponentInChildren<Slider>();

            text.text = party.Name;
            slider.value = party.Health;

            RectTransform playerPanelRectTransform = playerPanel.GetComponent<RectTransform>();


            RectTransform rectTransform = obj.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector2((playerPanelRectTransform.position.x * index) * distanceBetweenMembers, 0);
            
            playerPanelSequence.Add(obj);
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
