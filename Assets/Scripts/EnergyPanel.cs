using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPanel : MonoBehaviour
{
    GameStats gameStats;
    EnergyManager energyManager;
    AdManager adManager;

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
        
        if (adManager == null)
        {
            adManager = FindObjectOfType<AdManager>();
        }
    }

    public void AdFor1Energy()
    {
        if (energyManager.currentEnergy < energyManager.maxEnergy && adManager.adShown)
        {
            energyManager.currentEnergy++;
            energyManager.runOnce = false;
            adManager.adShown = false;
        }
        else
        {
            Debug.Log("no ad shown");
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
