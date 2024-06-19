using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Dialogue
{
    public class DialogueChoices : MonoBehaviour
    {
        private static DialogueChoices instance;

        [SerializeField] public List<string> tags;

        private void Start()
        {
            tags = new List<string>();
        }

        private void Awake()
        {
            if (instance != null) Debug.LogWarning("Another instance of the DialogueChoices is running.");
            instance = this;
        }

        public static DialogueChoices GetInstance() { return instance; }

        public void ParseTag(Story story)
        {
            Debug.Log("Parsing Tags");
            tags = story.currentTags;

            foreach (string t in tags)
            {
                string prefix = t.Split(' ')[0];
                string param = t.Split(' ')[1];

                switch (prefix.ToLower())
                {
                    // #reward sword | sword is the param, so basically give the player a sword if "reward" is called.
                    case "reward":
                        // give player medkit
                        switch (param.ToLower())
                        {
                            case "money":
                                Manager.GetInstance().increaseMoneyCount(Random.Range(0, 100));
                                break;
                        }
                        break;
                    case "harm":
                        Manager.GetInstance().decreaseUserHealth(int.Parse(param));
                        break;
                    case "open":
                        switch(param.ToLower())
                        {
                            case "shop":
                                Debug.Log("Opening Shop");
                                UIManager.GetInstance().OpenShopUI();
                                break;
                        }
                        break;
                }
            }
        }
    }
}
