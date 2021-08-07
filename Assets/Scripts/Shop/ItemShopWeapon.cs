using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopWeapon : MonoBehaviour
{
    public Text titleText;
    public Text descText;
    public Text priceText;

    public GameObject purchased;
    public GameObject player;

    public GameObject weapon;

    public string title;
    public string desc;
    public float price;

    public bool isPurchaseable = true;

    GameStats gameStats;

    private void Awake()
    {
        gameStats = FindObjectOfType<GameStats>();

        titleText.text = title;
        descText.text = desc;
        priceText.text = "$ " + price.ToString("F2");
    }

    private void Update()
    {
        if (!isPurchaseable)
        {
            purchased.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }

    public void BuyWeapon()
    {
        if (gameStats.playerMoney >= price && isPurchaseable)
        {
            isPurchaseable = false;
            gameStats.playerMoney -= price;
            gameStats.uiUpdated = true;
            Instantiate(weapon, player.transform);
        }
        else
        {
            //not enough money
        }
    }
}
