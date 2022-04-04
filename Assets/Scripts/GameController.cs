using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{    
    bool levelButtonClicked = false;
    bool backToMenuButtonClicked = false;
    bool shopButtonClicked = false;
    bool menuFromShopClicked = false;

    [SerializeField] 
    private GameObject playLevelsButton;
    [SerializeField] 
    private GameObject backButton;
    [SerializeField] 
    private GameObject shopButton;
    [SerializeField] 
    private GameObject charactersButton;
    [SerializeField] 
    private GameObject inventoryButton;
    [SerializeField] 
    private GameObject playerInfoBar;
    [SerializeField] 
    private GameObject leftButton;
    [SerializeField] 
    private GameObject rightButton;
    [SerializeField] 
    private GameObject selectButton;
    [SerializeField] 
    private GameObject playButton;
    [SerializeField]
    private GameObject bountiesText;
    [SerializeField]
    private GameObject levelPanel;
    [SerializeField]
    private GameObject detailedPanel;
    [SerializeField]
    private GameObject shopPanel;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject blackScreen;

    public bool screenClosed = false;

    float timer = 0;

    private void Awake()
    {
        levelButtonClicked = false;
        backToMenuButtonClicked = false;  
    }

    private void Start()
    {
        screenClosed = false;

        Invoke("CloseShopAtStart", 3.9f);
        Invoke("CloseBlackScreen", 4f);
    }

    void Update()
    {
        //Level poster screen
        if (levelButtonClicked)
        {
            if (LevelManagerSystem.Instance.CameraController.GetAnimationInfoLevel())
            {
                backButton.SetActive(true);
                bountiesText.SetActive(true);
                levelPanel.SetActive(true);
            }
        }

        if (shopButtonClicked)
        {
            if (LevelManagerSystem.Instance.CameraController.GetAnimationInfoShop())
            {
                shopPanel.SetActive(true);
            }
        }

        //Main character screen
        if (backToMenuButtonClicked || menuFromShopClicked)
        {            
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
                menuFromShopClicked = false;
                timer = 0;
            }
        }
    }

    public void LevelButton()
    {
        LevelManagerSystem.Instance.CameraController.LevelButtonAnimation();

        playLevelsButton.SetActive(false);
        shopButton.SetActive(false);
        charactersButton.SetActive(false);
        inventoryButton.SetActive(false);
        playerInfoBar.SetActive(false);

        levelButtonClicked = true;
        backToMenuButtonClicked = false;
    }

    public void BackToMenuButton()
    {
        LevelManagerSystem.Instance.CameraController.BackMenuButtonAnimation();

        backButton.SetActive(false);
        bountiesText.SetActive(false);
        levelPanel.SetActive(false);
        detailedPanel.SetActive(false);

        backToMenuButtonClicked = true;
        levelButtonClicked = false;
    }

    public void SelectButton()
    {
        playLevelsButton.SetActive(true);
        shopButton.SetActive(true);
        charactersButton.SetActive(true);
        inventoryButton.SetActive(true);
        playerInfoBar.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        selectButton.SetActive(false);
    }

    public void CharactersScreen()
    {
        playLevelsButton.SetActive(false);
        shopButton.SetActive(false);
        charactersButton.SetActive(false);
        inventoryButton.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        selectButton.SetActive(true);
    }

    public void ShopButton()
    {
        LevelManagerSystem.Instance.CameraController.ShopButtonAnimation();

        playLevelsButton.SetActive(false);
        shopButton.SetActive(false);
        charactersButton.SetActive(false);
        inventoryButton.SetActive(false);
        playerInfoBar.SetActive(false);

        shopButtonClicked = true;
        menuFromShopClicked = false;
    }

    public void MenuFromShopButton()
    {
        LevelManagerSystem.Instance.CameraController.BackFromShopAnimation();
        
        shopPanel.SetActive(false);

        shopButtonClicked = false;
        menuFromShopClicked = true;
    }

    public void InventoryOpen()
    {
        inventoryPanel.SetActive(true);
        shopButton.SetActive(false);
    }

    public void InventoryClose()
    {
        inventoryPanel.SetActive(false);
        shopButton.SetActive(true);
    }

    public void CloseShopAtStart()
    {
        shopPanel.SetActive(false);
    }

    public void CloseBlackScreen()
    {
        blackScreen.SetActive(false);
        screenClosed = true;
    }
}