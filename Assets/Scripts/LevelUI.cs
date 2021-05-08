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
    public GameObject detailedPanel;
    public GameObject lockImage;

    public Button playButton;

    [SerializeField]
    private Level level;

    public void SetLevel(Level _level)
    {
        level = _level;
    }

    public void DetailedLevelPage()
    {
        if (level.starsNeeded > GameStats.Instance.totalStars)
        {
            level.isLocked = true;
        }

        GameStats.Instance.levelReward = level.reward;

        enemyNameText.text = level.enemyName;
        rewardText.text = level.reward.ToString();

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
    }

    public void BackToLevelSelect()
    {
        detailedPanel.SetActive(false);
    }
}
