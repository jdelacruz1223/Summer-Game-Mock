using Assets.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class RandomEncounterManager : MonoBehaviour
    {
        [Header("Random Encounter")]
        [SerializeField] List<TextAsset> Stories;
        public int secondsPerEncounter;
        public float encounterPercentage;

        [Header("Food Check Encounter")]
        public int secondsPerFoodCheck;
        public float foodCheckPercentage;

        public bool currentlyInEncounter { get; set; }

        private static RandomEncounterManager instance;

        public static RandomEncounterManager GetInstance() { return instance; }
        private void Awake()
        {
            if (instance != null) Debug.LogWarning("Another instance of the RandomEncounterManager is running.");
            instance = this;
        }

        private void Start()
        {
            currentlyInEncounter = false;
            StartCoroutine(RandomEncounter());
            
            if (Manager.GetInstance().party.Count == 0) return;
            StartCoroutine(FoodCheck());
        }

        private void Update()
        {
            // Once it reaches somewhere 95% delete this script so that it doesn't spawn any more random encounters.
            if (Manager.GetInstance().currentProgress == 95) Destroy(this);
        }

        IEnumerator RandomEncounter()
        {
            while (true)
            {
                if (!currentlyInEncounter && Manager.GetInstance().currentProgress != 100)
                {
                    yield return new WaitForSeconds(secondsPerEncounter);
                    if (Random.value <= encounterPercentage / 100)
                    {
                        currentlyInEncounter = true;

                        // Select Randomly from the list of stories
                        int index = Random.Range(0, Stories.Count);
                        TextAsset inkJson = Stories[index];

                        Manager.GetInstance().increaseRandomEncounter();
                        DialogueManager.GetInstance().EnterEncounterDialogueMode(inkJson);
                    }
                }
                
                yield return null;
            }
        }

        IEnumerator FoodCheck()
        {
            while (true)
            {
                if (Manager.GetInstance().currentProgress != 100)
                {
                    yield return new WaitForSeconds(secondsPerFoodCheck);
                    if (Random.value <= foodCheckPercentage / 100)
                    {
                        List<PartyModel> partyList = Manager.GetInstance().party;

                        int index = Random.Range(0, partyList.Count);
                        PartyModel chosenMember = partyList[index];

                        Manager.GetInstance().decreaseSnackCount(1);
                        Manager.GetInstance().increaseHealthToMember(chosenMember.Name, 1);
                    }
                }
                yield return null;
            }
        }
    }
}
