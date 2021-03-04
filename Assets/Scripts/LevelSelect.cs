using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public string enemyName;
    public int reward;
    public int levelID;
    //bool levelComplete = false;
    public bool isLocked = true;

    public Image enemyImage;
    public Text enemyNameText;
    public Text rewardText;
    public GameObject detailedPanel;
    public GameObject lockImage;

    public SceneController sceneController;

    public void DetailedLevelPage()
    {
        enemyNameText.text = enemyName;
        rewardText.text = reward.ToString();
        sceneController.LevelID = levelID;

        detailedPanel.SetActive(true);
        if (isLocked)
        {
            lockImage.SetActive(true);
        }
        else
        {
            lockImage.SetActive(false);
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
