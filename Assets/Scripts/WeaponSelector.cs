using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponSelector : MonoBehaviour
{
    public int index;

    public GameObject currentButton;

    public WeaponSelection weaponSelection;
    //SetIndex[] setIndices;

    private void Update()
    {
        if (weaponSelection == null)
        {
            weaponSelection = FindObjectOfType<WeaponSelection>();
        }
    }

    public void SetNewIndex()
    {
        //for (int i = 0; i < setIndices.Length; i++)
        //{
        //    if (setIndices[i].gameObject.name == "Revolver 2")
        //    {
        //        newIndex = setIndices[i].weaponIndex;
        //    }
        //}
        index = currentButton.GetComponent<SetIndex>().weaponIndex;
        weaponSelection.EquipButton();
        weaponSelection.SelectWeapon();
    }

    public void ResetIndex(int _index)
    {
        index = _index;
        weaponSelection.EquipButton();
        weaponSelection.SelectWeapon();
    }
}
