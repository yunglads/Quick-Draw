﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{    
    public Animator mainCamera;

    bool levelButtonClicked = false;
    bool backToMenuButtonClicked = false;
    bool selectButtonClicked = false;

    public GameObject playLevelsButton;
    public GameObject backButton;
    public GameObject shopButton;
    public GameObject charactersButton;
    public GameObject inventoryButton;
    public GameObject playerInfoBar;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject selectButton;
    public GameObject playButton;

    public GameObject bountiesText;
    public GameObject levelPanel;
    public GameObject detailedPanel;


    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = GetComponent<Animator>();
        //PlayOpening();
    }

    public void PlayOpening()
    {
        mainCamera.Play("menuOpening");
    }

    // Update is called once per frame
    void Update()
    {
        //if (mainCamera.GetCurrentAnimatorStateInfo(0).IsTag("Menu"))
        //{
        //    playButton.SetActive(true);
        //    shopButton.SetActive(true);
        //    charactersButton.SetActive(true);
        //    inventoryButton.SetActive(true);
        //    playerInfoBar.SetActive(true);
        //}

        //Level poster screen
        if (levelButtonClicked)
        {
            playLevelsButton.SetActive(false);
            shopButton.SetActive(false);
            charactersButton.SetActive(false);
            inventoryButton.SetActive(false);
            playerInfoBar.SetActive(false);

            if (mainCamera.GetCurrentAnimatorStateInfo(0).IsTag("LevelBoard"))
            {
                backButton.SetActive(true);
                //level1Button.SetActive(true);
                bountiesText.SetActive(true);
                levelPanel.SetActive(true);

            }
        }
        //Main character screen
        if (backToMenuButtonClicked)
        {
            backButton.SetActive(false);
            //playButton.SetActive(false);
            bountiesText.SetActive(false);
            levelPanel.SetActive(false);
            detailedPanel.SetActive(false);
            
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                playLevelsButton.SetActive(true);
                shopButton.SetActive(true);
                charactersButton.SetActive(true);
                inventoryButton.SetActive(true);
                playerInfoBar.SetActive(true);
                //leftButton.SetActive(false);
                //rightButton.SetActive(false);
                backToMenuButtonClicked = false;
                timer = 0;
            }
        }

        if (selectButtonClicked)
        {
            playLevelsButton.SetActive(true);
            shopButton.SetActive(true);
            charactersButton.SetActive(true);
            inventoryButton.SetActive(true);
            playerInfoBar.SetActive(true);
            leftButton.SetActive(false);
            rightButton.SetActive(false);
            selectButton.SetActive(false);

            selectButtonClicked = false;
        }
    }

    public void LevelButton()
    {
        mainCamera.SetBool("levelButtonClicked", true);
        mainCamera.SetBool("backToMenuButtonClicked", false);
        levelButtonClicked = true;
        backToMenuButtonClicked = false;
    }

    public void BackToMenuButton()
    {
        mainCamera.SetBool("backToMenuButtonClicked", true);
        mainCamera.SetBool("levelButtonClicked", false);
        backToMenuButtonClicked = true;
        levelButtonClicked = false;
    }

    public void SelectButton()
    {
        selectButtonClicked = true;
    }

    public void CharactersScreen()
    {
        playLevelsButton.SetActive(false);
        //backButton.SetActive(true);
        shopButton.SetActive(false);
        charactersButton.SetActive(false);
        inventoryButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        selectButton.SetActive(true);
    }

    //public void LevelLoad()
    //{
    //    SceneManager.LoadScene("GunfightScene");
    //}
}
