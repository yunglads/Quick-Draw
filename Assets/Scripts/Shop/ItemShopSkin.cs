using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopSkin : MonoBehaviour
{
    public Text titleText;
    public Text descText;
    public Text priceText;

    public GameObject purchased;
    public GameObject player;

    public GameObject skin;

    public string title;
    public string desc;
    public float price;

    public bool isPurchaseable = true;

    GameStats gameStats;
    CharacterSelection characterSelection;

    private void Awake()
    {
        gameStats = FindObjectOfType<GameStats>();
        characterSelection = FindObjectOfType<CharacterSelection>();

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

    public void BuySkin()
    {
        if (gameStats.playerMoney >= price && isPurchaseable)
        {
            isPurchaseable = false;
            gameStats.playerMoney -= price;
            gameStats.uiUpdated = true;
            //characterSelection.characterList.Add(skin1);
            Instantiate(skin, player.transform);
            characterSelection.updateList = true;
        }
        else
        {
            //not enough money
        }
    }
}
