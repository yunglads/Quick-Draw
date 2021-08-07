using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public GameObject weaponsItems;
    public GameObject skinsItems;
    public GameObject goldItems;

    public void WeaponsButton()
    {
        weaponsItems.SetActive(true);
        skinsItems.SetActive(false);
        goldItems.SetActive(false);
    }

    public void SkinsButton()
    {
        weaponsItems.SetActive(false);
        skinsItems.SetActive(true);
        goldItems.SetActive(false);
    }

    public void GoldButton()
    {
        weaponsItems.SetActive(false);
        skinsItems.SetActive(false);
        goldItems.SetActive(true);
    }
}
