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
    public GameObject weaponList;

    public GameObject weapon;

    public string title;
    public string desc;
    public float price;
    public int buyOrder = 0;

    public bool isPurchaseable = true;

    GameStats gameStats;
    WeaponSelection weaponSelection;
    SetIndex[] setIndex;

    private void Start()
    {
        gameStats = FindObjectOfType<GameStats>();
        weaponSelection = FindObjectOfType<WeaponSelection>();

        weaponList = GameObject.FindGameObjectWithTag("Weapon");

        titleText.text = title;
        descText.text = desc;
        priceText.text = "$ " + price.ToString("F2");
    }

    private void Update()
    {
        if (weaponList == null)
        {
            weaponList = GameObject.FindGameObjectWithTag("Weapon");
        }

        if (setIndex == null)
        {
            setIndex = FindObjectsOfType<SetIndex>();
        }

        foreach (GameObject item in weaponList.GetComponent<WeaponSelection>().weaponList)
        {
            if (weapon.name == item.name)
            {
                isPurchaseable = false;
            }

            Debug.Log(weapon.name + " + " + item.name);
        }

        if (!isPurchaseable)
        {
            purchased.SetActive(true);
            GetComponent<Button>().interactable = false;
            isPurchaseable = false;
        }
    }

    public void BuyWeapon()
    {
        if (gameStats.playerMoney >= price && isPurchaseable)
        {
            for (int i = 0; i < setIndex.Length; i++)
            {
                setIndex[i].checkList = true;
            }

            isPurchaseable = false;
            gameStats.playerMoney -= price;
            gameStats.uiUpdated = true;
            GameObject go = Instantiate(weapon, weaponList.transform);
            go.name = weapon.name;
            weaponSelection.updateList = true;
        }
    }
}
