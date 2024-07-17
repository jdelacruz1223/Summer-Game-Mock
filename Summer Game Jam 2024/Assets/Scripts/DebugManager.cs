using UnityEngine;

public class DebugManager : Manager
{
    [Header("Scene Setup")]
    public bool dialogueManager = true; 
    public bool inputManager = true;
    public bool uiManager = true;

    /// <summary>
    /// Scene Setup Variables
    /// </summary>
    public bool hasDialogueManager { get; private set; }
    public bool hasInputManager { get; private set; }
    public bool hasUIManager { get; private set; }

    public static new DebugManager GetInstance() { return me; }

    private static new DebugManager me;  

    void Awake()
    {
        if (Manager.me != null && Manager.me != this)
        {
            Destroy(gameObject);
            return;
        }

        me = this;
        Manager.me = me;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        hasDialogueManager = true;
        hasInputManager = true;
        hasUIManager = true;

        SetupManager();
    }

    void SetupManager()
    {
        if (!dialogueManager) hasDialogueManager = false;
        if (!inputManager) hasInputManager = false;
        if (!uiManager) hasUIManager = false;
    }
}
