using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPanel : MonoBehaviour
{
    GameStats gameStats;
    EnergyManager energyManager;

    // Update is called once per frame
    void Update()
    {
        if (gameStats == null)
        {
            gameStats = FindObjectOfType<GameStats>();
        }

        if (energyManager == null)
        {
            energyManager = FindObjectOfType<EnergyManager>();
        }
    }

    public void AdFor1Energy()
    {
        if (energyManager.currentEnergy < energyManager.maxEnergy)
        {
            energyManager.currentEnergy++;
            energyManager.runOnce = false;
        }
    }

    public void CashFor1Energy()
    {
        if (energyManager.currentEnergy < energyManager.maxEnergy && gameStats.playerMoney >= 100)
        {
            gameStats.playerMoney -= 100;
            gameStats.UpdateUI();
            energyManager.currentEnergy++;
            energyManager.runOnce = false;
        }
    }
}
