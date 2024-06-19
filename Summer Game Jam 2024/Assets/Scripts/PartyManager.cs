using Assets.Model;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    [Header("Party Members")]
    public List<GameObject> partyPlayers;
    public float depletionRate = 1f;

    void Start()
    {
        SyncData();
        StartCoroutine(DepleteHP());
    }

    void SyncData()
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

    void Update()
    {

    }


    IEnumerator DepleteHP()
    {
        while (true)
        {
            if (Manager.GetInstance().currentProgress == 100) Destroy(this);

            int randWait = Random.Range(1, 3);
            yield return new WaitForSeconds(randWait); // Wait for 1 second
            List<PartyModel> partyList = Manager.GetInstance().party;

            for (int i = 0; i < partyList.Count; i++)
            {
                var player = partyList[i];
                Manager.GetInstance().decreaseHealthToMember(player.Name, depletionRate);


                if (player.Health < 0)
                {
                    player.Health = 0; // Ensure HP doesn't drop below 0
                }

                // Update the UI
                var partyIndex = partyPlayers[i];
                var hpBar = partyIndex.GetComponentInChildren<Slider>();
                hpBar.value = player.Health / 100;
            }
        }
    }

}
