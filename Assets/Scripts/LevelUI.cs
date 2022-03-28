using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{    
    public int levelID;

    public Image enemyImage;
    public Sprite enemySprite;
    public Text enemyNameText;
    public Text rewardText;
    public GameObject capturedText;
    public GameObject detailedPanel;
    public GameObject lockImage;
    public GameObject completedImage;
    public GameObject star1;
    public GameObject star2;
    public GameObject star2Gray;


    public Button playButton;

    [SerializeField]
    private Level level;

    public void SetLevel(Level _level)
    {
        level = _level;
    }

    public void Update()
    {
        if (level.levelCompleted)
        {
            completedImage.SetActive(true);
        }
        else
        {
            completedImage.SetActive(false);
        }
    }

    public void DetailedLevelPage()
    {
        if (level.starsNeeded > GameStats.Instance.totalStars)
        {
            level.isLocked = true;
        }

        enemyNameText.text = level.enemyName;
        rewardText.text = level.levelReward.ToString();

        LevelManagerSystem.Instance.SetCurrentLevel(levelID);

        enemyImage.sprite = enemySprite;

        detailedPanel.SetActive(true);

        if (level.isLocked)
        {
            lockImage.SetActive(true);
            playButton.interactable = false;
        }
        else
        {
            lockImage.SetActive(false);
            playButton.interactable = true;
        }

        if (level.levelCompleted)
        {
            capturedText.SetActive(true);
        }
        else
        {
            capturedText.SetActive(false);
        }

        if (level.stars == 1)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star2Gray.SetActive(true);
        }
        else if (level.stars == 2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star2Gray.SetActive(false);
        }
        else
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star2Gray.SetActive(false);
        }
    }

    public void BackToLevelSelect()
    {
        detailedPanel.SetActive(false);
    }
}
