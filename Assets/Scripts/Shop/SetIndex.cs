using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIndex : MonoBehaviour
{
    public int weaponIndex;

    public bool checkList = false;

    WeaponSelector weaponSelector;
    WeaponSelection weaponSelection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSelector == null)
        {
            weaponSelector = FindObjectOfType<WeaponSelector>();
        }

        if (weaponSelection == null)
        {
            weaponSelection = FindObjectOfType<WeaponSelection>();
        }

        if (checkList)
        {
            //Invoke("CheckForWeapon", 1f);

            for (int i = 0; i < weaponSelection.weaponList.Count; i++)
            {
                if (weaponSelection.weaponList[i].name == gameObject.name)
                {
                    weaponIndex = i;
                    Debug.Log(gameObject.name + " set to index : " + i);
                }
            }

            checkList = false;
        }
    }

    public void SetAsButton()
    {
        weaponSelector.currentButton = gameObject;
    }

    //void SetWeaponIndex()
    //{
    //    weaponSelector.newIndex = weaponIndex;
    //}

    //void CheckForWeapon()
    //{
    //    Debug.Log("Checking weapon list");

    //    for (int i = 0; i < weaponSelection.weaponList.Count; i++)
    //    {
    //        if (weaponSelection.weaponList[i].name == gameObject.name)
    //        {
    //            weaponIndex = i;
    //            Debug.Log(gameObject.name + " set to index : " + i);
    //        }
    //    }

    //    checkList = false;
    //}
}
