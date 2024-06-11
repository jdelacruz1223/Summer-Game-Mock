using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInputActions playerControls;
    void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.Enable();
    }
}
