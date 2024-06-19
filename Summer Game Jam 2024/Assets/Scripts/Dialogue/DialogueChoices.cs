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

            var instance = Manager.GetInstance();

            foreach (string t in tags)
            {
                string prefix = t.Split(' ')[0];
                string param = t.Split(' ')[1];

                switch (prefix.ToLower())
                {
                    // #reward sword | sword is the param, so basically give the player a sword if "reward" is called.
                    case "reward":
                        switch (param.ToLower())
                        {
                            case "money":
                               instance.increaseMoneyCount(Random.Range(0, 100));
                                break;
                            case "medicine":
                                instance.increaseMedicineCount(1);
                                break;
                            case "food":
                                instance.increaseSnackCount(1);
                                break;
                        }
                        break;
                    case "harm":
                        instance.decreaseUserHealth(int.Parse(param));
                        break;

                    case "tires":
                        Debug.Log("Dialogue Choices: " + param);
                        if (param.Contains("-"))
                        {
                            Debug.Log("result: " + param);
                            instance.decreaseTireCount(int.Parse(param.ToString().Replace("-", "")));
                        }
                        else
                            instance.increaseTireCount(int.Parse(param));
                        break;

                    case "food":
                        if (param.Contains("-"))
                            instance.decreaseSnackCount(int.Parse(param.ToString().Replace("-", "")));
                        else
                            instance.increaseSnackCount(int.Parse(param));
                        break;

                    case "harmparty":
                        instance.changeHealthToParty(-int.Parse(param));
                        break;

                    case "happyparty":
                        instance.changeHealthToParty(int.Parse(param));
                        break;
                    case "medicine":
                        if (param.Contains("-"))
                            instance.decreaseMedicineCount(int.Parse(param.ToString().Replace("-", "")));
                        else
                            instance.increaseMedicineCount(int.Parse(param));
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
