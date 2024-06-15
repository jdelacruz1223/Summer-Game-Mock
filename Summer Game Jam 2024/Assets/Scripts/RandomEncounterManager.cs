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

        public bool currentlyInEncounter { get; private set; }

        private static RandomEncounterManager instance;

        public static RandomEncounterManager GetInstance() { return instance; }
        private void Awake()
        {
            if (instance != null) Debug.LogWarning("Another instance of the RandomEncounterManager is running.");
            instance = this;

            StartCoroutine(RandomEncounter());
        }

        IEnumerator RandomEncounter()
        {
            yield return new WaitForSeconds(5);

            // Select Randomly from the list of stories
            int index = Random.Range(0, Stories.Count);
            TextAsset inkJson = Stories[index];

            DialogueManager.GetInstance().EnterEncounterDialogueMode(inkJson);
        }
    }
}
