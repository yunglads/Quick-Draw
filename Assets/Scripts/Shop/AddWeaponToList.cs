using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWeaponToList : MonoBehaviour
{
    //public GameObject weaponTemplate;
    //public GameObject weaponTab;
    //GameObject instantiatedWeapon;
    public GameObject itemLock;
    public Button item;

    //public string weaponName;
    //public Sprite weaponImage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockItem()
    {
        //Instantiate(weaponTemplate, weaponTab.transform);
        //instantiatedWeapon.name = weaponName;
        //instantiatedWeapon.GetComponentInChildren<Text>().text = weaponName;
        //instantiatedWeapon.transform.Find("Image").GetComponentInChildren<Image>().sprite = weaponImage;
        itemLock.SetActive(false);
        item.interactable = true;

    }
}
