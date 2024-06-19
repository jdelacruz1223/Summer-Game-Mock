using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject suppliesMenuObject;
    private GameObject currentPanel;
    public void EndButton()
    {
        Application.Quit();
    }

    public void SuppliesMenu()
    {
        currentPanel = suppliesMenuObject;
        suppliesMenuObject.SetActive(true);
    }

    public void ClosePanel()
    {
        currentPanel.SetActive(false);
    }
}
