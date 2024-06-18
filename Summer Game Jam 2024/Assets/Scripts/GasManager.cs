using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasManager : MonoBehaviour
{
    public float gasTankCapacity = 100f;
    public float burnRatePerSecond = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Manager.GetInstance().setGasCount(gasTankCapacity);

        // Start the burning process
        InvokeRepeating("BurnGas", 1f, 1f);
    }

    void BurnGas()
    {
        if (RandomEncounterManager.GetInstance().currentlyInEncounter) return;

        var managerVar = Manager.GetInstance();
        var currentGasLevel = managerVar.gasNum;

        if (currentGasLevel > 0)
        {
            // Burn the gas according to the burn rate
            managerVar.decreaseGasCount(1);

            // Ensure the gas level does not drop below zero
            if (currentGasLevel < 0)
            {
                managerVar.setGasCount(0);
            }

            // Log the current gas level to the console
            Debug.Log("Current Gas Level: " + currentGasLevel + " liters");
        }
        else
        {
            // Stop burning gas if the tank is empty
            CancelInvoke("BurnGas");
            Debug.Log("The gas tank is empty!");
        }
    }
}
