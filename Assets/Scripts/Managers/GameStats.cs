using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameStats : Singleton<GameStats>
{
    [SerializeField]
    public int totalStars = 0;
    [SerializeField]
    public float playerMoney = 0;
    [SerializeField]
    public int playerGold = 0;

    public List<string> tempSkins;

    public bool uiUpdated = false;

    [SerializeField]
    private Text playerMoneyText;
    [SerializeField]
    private Text playerGoldText;
    [SerializeField]
    private Text totalStarsText;

    public Camera mainCamera;

    public CharacterSelection characterSelection;

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

        if (characterSelection == null)
        {
            characterSelection = FindObjectOfType<CharacterSelection>();
        }

        if (tempSkins.Count < characterSelection.characterList.Count)
        {
            tempSkins = new List<string>();

            for (int i = 0; i < characterSelection.characterList.Count; i++)
            { 
                tempSkins.Add(characterSelection.characterList[i].name);
            }
        }
    }

    public void UpdateStars(int stars)
    {
        totalStars += stars;
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
        totalStarsText.text = totalStars.ToString();

        //PlayerData data = new PlayerData();
        //data.savedTotalStars = totalStars;
        //data.savedPlayerMoney = playerMoney;
        //data.savedPlayerGold = playerGold;
        //SaveSystem.SavePlayerData(data);

        //uiUpdated = true;
        Debug.Log("UI found");
    }

    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();
        data.savedTotalStars = totalStars;
        data.savedPlayerMoney = playerMoney;
        data.savedPlayerGold = playerGold;
        data.savedLevels = FindObjectOfType<LevelManagerSystem>().levels;

        data.savedSkins = tempSkins;
        for (int i = 0; i < data.savedSkins.Count; i++)
        {
            Debug.Log("Saved skin: " + data.savedSkins[i]);
        }

        SaveSystem.SavePlayerData(data);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);

            PlayerData saveData = (PlayerData)formatter.Deserialize(stream);
            totalStars = saveData.savedTotalStars;
            playerMoney = saveData.savedPlayerMoney;
            playerGold = saveData.savedPlayerGold;
            FindObjectOfType<LevelManagerSystem>().levels = saveData.savedLevels;

            for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").transform.childCount; i++)
            {
                Destroy(GameObject.FindGameObjectWithTag("Player").transform.GetChild(i).gameObject);
            }

            characterSelection.characterList.Clear();

            for (int i = 0; i < saveData.savedSkins.Count; i++)
            {
                //characterSelection.characterList.Add(Resources.Load<GameObject>("Characters/" + saveData.savedSkins[i]));
                GameObject go = Instantiate(Resources.Load<GameObject>("Characters/" + saveData.savedSkins[i]), GameObject.FindGameObjectWithTag("Player").transform);
                go.name = saveData.savedSkins[i];
                //characterSelection.updateList = true;
                Debug.Log("Loaded Skin: " + saveData.savedSkins[i]);
            }

            characterSelection.updateList = true;


            stream.Close();

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}