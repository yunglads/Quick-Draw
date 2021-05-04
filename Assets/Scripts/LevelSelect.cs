using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public string enemyName;
    public int reward;
    public int levelID;
    public int starsNeeded;
    //public bool levelComplete = false;
    public bool isLocked = true;
    public Image enemyImage;

    public Sprite enemySprite;
    public Text enemyNameText;
    public Text rewardText;
    public GameObject detailedPanel;
    public GameObject lockImage;

    public Button playButton;

    public GameStats gameStats;

    private void Update()
    {
        if (gameStats == null)
        {
            gameStats = FindObjectOfType<GameStats>();
        }

        if (starsNeeded > gameStats.totalStars)
        {
            isLocked = true;
        }
    }

    public void DetailedLevelPage()
    {
        gameStats.levelReward = reward;

        enemyNameText.text = enemyName;
        rewardText.text = reward.ToString();

        //sceneController.LevelID = levelID;
        LevelManagerSystem.Instance.SetCurrentLevel(levelID);

        enemyImage.sprite = enemySprite;

        detailedPanel.SetActive(true);

        if (isLocked)
        {
            lockImage.SetActive(true);
            playButton.interactable = false;
        }
        else
        {
            lockImage.SetActive(false);
            playButton.interactable = true;
        }

        //enemyImage.gameObject.SetActive(true);
        //enemyNameText.gameObject.SetActive(true);
        //rewardText.gameObject.SetActive(true);
    }

    public void BackToLevelSelect()
    {
        detailedPanel.SetActive(false);
    }
}
