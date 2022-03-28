using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minus1Energy : MonoBehaviour
{
    EnergyManager energyManager;
    //bool isClicked = false;

    private void Update()
    {
        if (energyManager == null)
        {
            energyManager = FindObjectOfType<EnergyManager>();
        }
    }

    public void MinusOneEnergy()
    {
        energyManager.currentEnergy--;
    }
}
