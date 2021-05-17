using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : Singleton<GameStats>
{
    [SerializeField]
    public int TotalStars = 0;
    [SerializeField]
    private float playerMoney = 0;
    [SerializeField]
    private int playerGold = 0;

    [SerializeField]
    private Text playerMoneyText;
    [SerializeField]
    private Text playerGoldText;
    [SerializeField]
    private Text totalStarsText;

    //bool playerUIFound = false;

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
            UpdateUI();
        }
    }

    private void Update()
    {
        if (playerGoldText == null && playerMoneyText == null && totalStarsText == null)
        {
            UpdateUI();
        }
    }

    public void UpdateStars(int starts)
    {
        TotalStars += starts;
    }

    public void UpdateMoney(float money)
    {
        playerMoney += money;
    }

    public void UpdateGold(int gold)
    {
        playerGold += gold;
    }

    public void UpdateUI()
    {
        playerMoneyText = GameObject.FindGameObjectWithTag("MoneyUI").GetComponent<Text>();
        playerGoldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<Text>();
        totalStarsText = GameObject.FindGameObjectWithTag("StarsUI").GetComponent<Text>();

        playerMoneyText.text = "$ " + playerMoney.ToString("F2");
        playerGoldText.text = playerGold.ToString();
        totalStarsText.text = TotalStars.ToString();
        Debug.Log("UI found");
    }
}