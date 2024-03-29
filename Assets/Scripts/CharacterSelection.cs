﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> characterList;

    [HideInInspector]
    public bool updateList = false;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

        characterList = new List<GameObject>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList.Add(transform.GetChild(i).gameObject);
        }

        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
    }

    private void Update()
    {
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }

        if (updateList)
        {
            UpdateList();
        }
    }

    public void LeftButton()
    {
        characterList[index].SetActive(false);

        index--;
        if(index < 0)
        {
            index = characterList.Count - 1;
        }

        characterList[index].SetActive(true);
    }

    public void RightButton()
    {
        characterList[index].SetActive(false);

        index++;
        if(index == characterList.Count)
        {
            index = 0;
        }

        characterList[index].SetActive(true);
    }

    public void UpdateList()
    {
        characterList = new List<GameObject>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }
        updateList = false;
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
    }
}
