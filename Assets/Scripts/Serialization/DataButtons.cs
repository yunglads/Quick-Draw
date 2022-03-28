using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataButtons : MonoBehaviour
{
    public GameStats gameStats;
    //public LevelManagerSystem levelManager;

    private void Update()
    {
        if (gameStats == null)
        {
            gameStats = FindObjectOfType<GameStats>();
        }

        //if (levelManager == null)
        //{
        //    levelManager = FindObjectOfType<LevelManagerSystem>();
        //}
    }

    public void SaveButton()
    {
        gameStats.SavePlayerData();

        //levelManager.SaveLevelData();
    }

    //public void CopyButton()
    //{
    //    gameData.CopyData();
    //}

    public void LoadButton()
    {
        //levelManager.LoadLevelData();

        gameStats.LoadPlayerData();
        gameStats.UpdateUI();
    }
}
