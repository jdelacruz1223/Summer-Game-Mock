using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInputActions playerControls;

    private void Start()
    {
        if (DebugManager.GetInstance() != null) if (!DebugManager.GetInstance().inputManager) { Destroy(this.gameObject); return; };
    }

    void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.Enable();
    }
}
