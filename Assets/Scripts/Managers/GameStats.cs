using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : Singleton<GameStats>
{
    public int totalStars = 0;
    public float playerMoney = 0;
    public int playerGold = 0;

    public int levelReward;
    public bool statUpdate = false;

    [SerializeField]
    private Text playerMoneyText;
    [SerializeField]
    private Text playerGoldText;
    [SerializeField]
    private Text totalStarsText;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            playerMoneyText = GameObject.FindGameObjectWithTag("MoneyUI").GetComponent<Text>();
            playerGoldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<Text>();
            totalStarsText = GameObject.FindGameObjectWithTag("StarsUI").GetComponent<Text>();

            playerMoneyText.text = playerMoney.ToString("F2");
            playerGoldText.text = playerGold.ToString();
            totalStarsText.text = totalStars.ToString();
            Debug.Log("UI found");
        }
    }
}