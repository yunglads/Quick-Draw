using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WeaponSelection : MonoBehaviour
{
    public List<GameObject> weaponList;


    [HideInInspector]
    public bool updateList = false;
    public bool weaponPosUpdated = false;

    public int index;

    public WeaponSelector weaponSelector;

    // Start is called before the first frame update
    void Start()
    {
        weaponSelector = FindObjectOfType<WeaponSelector>();

        //Use below code to reset index if index gets stuck on "out of range"
        PlayerPrefs.SetInt("WeaponSelected", 0);

        index = PlayerPrefs.GetInt("WeaponSelected");

        weaponList = new List<GameObject>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            weaponList.Add(transform.GetChild(i).gameObject);
        }

        if (weaponList[index])
        {
            weaponList[index].SetActive(true);
        }
    }

    private void Update()
    {
        if (weaponSelector == null)
        {
            weaponSelector = FindObjectOfType<WeaponSelector>();
        }

        if (weaponList[index])
        {
            weaponList[index].SetActive(true);
        }

        if (updateList)
        {
            UpdateList();
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu") && !weaponPosUpdated)
        {
            ResetWeaponPos();
            //Debug.Log("Resetting weapon position!!");
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            weaponPosUpdated = false;

            foreach (GameObject weapon in weaponList)
            {
                weapon.GetComponent<Animator>().enabled = true;
            }
        }
    }

    public void EquipButton()
    {
        weaponList[index].SetActive(false);

        index = weaponSelector.index;

        weaponList[index].SetActive(true);
    }

    //public void RightButton()
    //{
    //    weaponList[index].SetActive(false);

    //    index++;
    //    if (index == weaponList.Count)
    //    {
    //        index = 0;
    //    }

    //    weaponList[index].SetActive(true);
    //}

    public void UpdateList()
    {
        weaponList = new List<GameObject>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            weaponList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject go in weaponList)
        {
            go.SetActive(false);
        }
        updateList = false;
    }

    public void SelectWeapon()
    {
        PlayerPrefs.SetInt("WeaponSelected", index);
    }

    void ResetWeaponPos()
    {
        foreach (GameObject weapon in weaponList)
        {
            weapon.transform.position = gameObject.transform.position;
            weapon.transform.rotation = gameObject.transform.rotation;

            weapon.GetComponent<Animator>().enabled = false;

            Debug.Log(weapon.name);
        }

        weaponPosUpdated = true;
        //Debug.Log("Resetting weapon position!!");
    }
}
