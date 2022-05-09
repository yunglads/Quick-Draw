using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPShop : MonoBehaviour
{
    private string goldPack1 = "com.spitfiregames.quickdraw.goldpack1";
    private string removeAds = "com.spitfiregames.quickdraw.noads";
    private string energyDrink = "com.spitfiregames.quickdraw.maxenergy";
    private string cashPouch = "com.spitfiregames.quickdraw.cashpouch";
    private string cashBag = "com.spitfiregames.quickdraw.cashbag";
    private string cashVault = "com.spitfiregames.quickdraw.cashvault";
    private string ownTheBank = "com.spitfiregames.quickdraw.ownthebank";
    private string createEnemy = "com.spitfiregames.quickdraw.createenemy";

    public GameObject restoreButton;

    GameStats gamestats;
    EnergyManager energyManager;

    private void Awake()
    {
        DisableRestoreButton();

        gamestats = FindObjectOfType<GameStats>();
        energyManager = FindObjectOfType<EnergyManager>();
    }

    public void OnPurchaseComplete(Product product)
    {
        //if (product.definition.id == goldPack1)
        //{
        //    gamestats.playerGold += 100;
        //    gamestats.uiUpdated = true;
        //}
        
        if (product.definition.id == removeAds)
        {
            gamestats.GetComponentInChildren<AdDisabler>().adsDisabled = true;
        }

        if (product.definition.id == energyDrink)
        {
            energyManager.currentEnergy = energyManager.maxEnergy;
            energyManager.runOnce = false;
        }

        if (product.definition.id == cashPouch)
        {
            gamestats.playerMoney += 250;
            gamestats.uiUpdated = true;
        }

        if (product.definition.id == cashBag)
        {
            gamestats.playerMoney += 500;
            gamestats.uiUpdated = true;
        }

        if (product.definition.id == cashVault)
        {
            gamestats.playerMoney += 1000;
            gamestats.uiUpdated = true;
        }

        if (product.definition.id == ownTheBank)
        {
            gamestats.playerMoney += 10000;
            gamestats.uiUpdated = true;
        }

        if (product.definition.id == createEnemy)
        {

        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of " + product.definition.id + " failed due to " + reason);
    }

    private void DisableRestoreButton()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }
}
