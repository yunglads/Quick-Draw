using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsPurchased : MonoBehaviour
{
    public GameObject item;
    public GameObject lockUI;
    public Button itemButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!item.GetComponent<ItemShopWeapon>().isPurchaseable)
        {
            lockUI.SetActive(false);
            itemButton.interactable = true;
        }
    }
}
