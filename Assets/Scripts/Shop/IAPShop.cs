using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPShop : MonoBehaviour
{
    private string goldPack1 = "com.spitfiregames.quickdraw.goldpack1";
    private string removeAds = "com.spitfiregames.quickdraw.noads";

    public GameObject restoreButton;

    GameStats gamestats;

    private void Awake()
    {
        DisableRestoreButton();

        gamestats = FindObjectOfType<GameStats>();
    }

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == goldPack1)
        {
            gamestats.playerGold += 100;
            gamestats.uiUpdated = true;
        }
        
        if (product.definition.id == removeAds)
        {
            gamestats.GetComponentInChildren<AdDisabler>().adsDisabled = true;
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
