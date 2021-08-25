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
    public float playerMoney = 0;
    [SerializeField]
    public int playerGold = 0;

    public bool uiUpdated = false;

    [SerializeField]
    private Text playerMoneyText;
    [SerializeField]
    private Text playerGoldText;
    [SerializeField]
    private Text totalStarsText;

    public Camera mainCamera;

    //bool playerUIFound = false;

    void Start()
    {
        Application.targetFrameRate = 65;
    }

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
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        if (uiUpdated && mainCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Menu"))
        {
            UpdateUI();
            uiUpdated = false;
        }

        if (playerGoldText == null && playerMoneyText == null && totalStarsText == null && mainCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Menu"))
        {
            UpdateUI();
            uiUpdated = false;
        }
    }

    public void UpdateStars(int stars)
    {
        TotalStars += stars;
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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerMoneyText = GameObject.FindGameObjectWithTag("MoneyUI").GetComponent<Text>();
        playerGoldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<Text>();
        totalStarsText = GameObject.FindGameObjectWithTag("StarsUI").GetComponent<Text>();

        playerMoneyText.text = "$ " + playerMoney.ToString("F2");
        playerGoldText.text = playerGold.ToString();
        totalStarsText.text = TotalStars.ToString();

        //uiUpdated = true;
        Debug.Log("UI found");
    }
}