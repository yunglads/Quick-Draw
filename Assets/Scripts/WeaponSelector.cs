using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelector : MonoBehaviour
{
    public int index;

    public WeaponSelection weaponSelection;

    private void Update()
    {
        if (weaponSelection == null)
        {
            weaponSelection = FindObjectOfType<WeaponSelection>();
        }   
    }

    public void SetNewIndex(int newIndex)
    {
        index = newIndex;
        weaponSelection.EquipButton();
        weaponSelection.SelectWeapon();
    } 
}
