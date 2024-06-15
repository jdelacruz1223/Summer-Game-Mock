using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class RandomEncounterManager : MonoBehaviour
    {
        [SerializeField] List<TextAsset> Stories;
        public int secondsPerEncounter;
        public float encounterPercentage;

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

                        DialogueManager.GetInstance().EnterEncounterDialogueMode(inkJson);
                    }
                }
                yield return null;
            }
        }
    }
}
