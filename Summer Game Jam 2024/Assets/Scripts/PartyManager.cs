using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    [Header("Party Members")]
    public List<GameObject> partyPlayers;

    void Start()
    {
        List<PartyModel> partyList = Manager.GetInstance().party;

        if (partyList.Count == 0) return;
        Debug.Log(partyPlayers);
        
        int index = 0;
        foreach (var player in partyList)
        {
            Debug.Log(player.Name);

            var partyIndex = partyPlayers[index];
            var nameTxt = partyIndex.GetComponentInChildren<TextMeshProUGUI>();
            var hpBar = partyIndex.GetComponentInChildren<Slider>();

            nameTxt.text = player.Name;
            hpBar.value = player.Health / 100;

            partyPlayers[index].SetActive(true);

            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
