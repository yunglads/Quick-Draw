using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{    
    bool levelButtonClicked = false;
    bool backToMenuButtonClicked = false;

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

    public GameObject bountiesText;
    public GameObject levelPanel;
    public GameObject detailedPanel;

    float timer = 0;

    void Update()
    {
        //Level poster screen
        if (levelButtonClicked)
        {
            if (LevelManagerSystem.Instance.CameraController.GetCurrentAnimationInfo())
            {
                backButton.SetActive(true);
                bountiesText.SetActive(true);
                levelPanel.SetActive(true);
            }
        }

        //Main character screen
        if (backToMenuButtonClicked)
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
}