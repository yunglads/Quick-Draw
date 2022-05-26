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
    //[SerializeField]
    //public int playerGold = 0;

    public List<string> tempSkins;
    public List<string> tempGuns;

    public bool uiUpdated = false;
    bool loadFinished = false;

    [SerializeField]
    private Text playerMoneyText;
    //[SerializeField]
    //private Text playerGoldText;
    [SerializeField]
    private Text totalStarsText;

    public Camera mainCamera;
    public Camera mainCameraLevels;

    public CharacterSelection characterSelection;
    public WeaponSelection weaponSelection;
    EnergyManager energyManager;

    void Start()
    {
        Application.targetFrameRate = 60;
        //uncomment to load on start
        //Invoke("LoadPlayerData", .5f);
        Invoke("UpdateUI", 3.9f);
        Invoke("WaitForLoadToFinish", 4f);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnApplicationQuit()
    {
        //uncomment to save on exit 
        SavePlayerData();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if (scene.name == "MainMenu" && loadFinished)
        //{
        //    UpdateUI();
        //}

        if (scene.name != "MainMenu")
        {
            loadFinished = false;
        }
    }

    private void Update()
    {
        //if (mainCamera == null)
        //{
        //    mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //}

        if (/*playerGoldText == null &&*/ playerMoneyText == null && totalStarsText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu") && mainCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Menu") || mainCameraLevels.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Menu"))
        {
            UpdateUI();
            uiUpdated = false;
            //Debug.Log("looking for text elements");
        }

        if (energyManager == null)
        {
            energyManager = FindObjectOfType<EnergyManager>();
        }

        if (uiUpdated && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu") && mainCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Menu") || mainCameraLevels.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Menu"))
        {
            UpdateUI();
            uiUpdated = false;
        }

        if (uiUpdated && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu") && mainCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Shop") || mainCameraLevels.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Shop"))
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

        if(weaponSelection == null)
        {
            weaponSelection = FindObjectOfType<WeaponSelection>();
        }

        if (tempGuns.Count < weaponSelection.weaponList.Count)
        {
            tempGuns = new List<string>();

            for (int i = 0; i < weaponSelection.weaponList.Count; i++)
            {
                tempGuns.Add(weaponSelection.weaponList[i].name);
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
        //playerGold += gold;
    }

    public void UpdateUI()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerMoneyText = GameObject.FindGameObjectWithTag("MoneyUI").GetComponent<Text>();
        //playerGoldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<Text>();
        totalStarsText = GameObject.FindGameObjectWithTag("StarsUI").GetComponent<Text>();

        playerMoneyText.text = "$ " + playerMoney.ToString("F2");
        //playerGoldText.text = playerGold.ToString();
        totalStarsText.text = totalStars.ToString();

        energyManager.UpdateEnergy();
        energyManager.UpdateTimer();
        energyManager.runOnce = false;

        //PlayerData data = new PlayerData();
        //data.savedTotalStars = totalStars;
        //data.savedPlayerMoney = playerMoney;
        //data.savedPlayerGold = playerGold;
        //SaveSystem.SavePlayerData(data);

        //uiUpdated = true;

        //Debug.Log("UI found");
    }

    public void SavePlayerData()
    {
        FindObjectOfType<EnergyManager>().Save();
        PlayerData data = new PlayerData();
        data.savedTotalStars = totalStars;
        data.savedPlayerMoney = playerMoney;
        //data.savedPlayerGold = playerGold;
        //data.savedEnergy = FindObjectOfType<EnergyManager>().currentEnergy;
        //data.savedNextEnergyTime = FindObjectOfType<EnergyManager>().nextEnergyTime.ToBinary();
        //data.savedLastAddedTime = FindObjectOfType<EnergyManager>().lastAddedTime.ToBinary();
        data.savedLevels = FindObjectOfType<LevelManagerSystem>().levels;
 
        data.savedSkins = tempSkins;
        //for (int i = 0; i < data.savedSkins.Count; i++)
        //{
        //    Debug.Log("Saved skin: " + data.savedSkins[i]);
        //}

        data.savedGuns = tempGuns;
        //for (int i = 0; i < data.savedGuns.Count; i++)
        //{
        //    Debug.Log("Saved skin: " + data.savedGuns[i]);
        //}

        SaveSystem.SavePlayerData(data);
    }

    public void LoadPlayerData()
    {
        FindObjectOfType<EnergyManager>().Load();
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(path, FileMode.Open);

            PlayerData saveData = (PlayerData)formatter.Deserialize(stream);
            totalStars = saveData.savedTotalStars;
            playerMoney = saveData.savedPlayerMoney;
            //playerGold = saveData.savedPlayerGold;
            //FindObjectOfType<EnergyManager>().currentEnergy = saveData.savedEnergy;
            //FindObjectOfType<EnergyManager>().nextEnergyTime = DateTime.FromBinary(saveData.savedNextEnergyTime);
            //FindObjectOfType<EnergyManager>().lastAddedTime = DateTime.FromBinary(saveData.savedLastAddedTime);
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

            for (int i = 0; i < GameObject.FindGameObjectWithTag("Weapon").transform.childCount; i++)
            {
                Destroy(GameObject.FindGameObjectWithTag("Weapon").transform.GetChild(i).gameObject);
            }

            weaponSelection.weaponList.Clear();

            for (int i = 0; i < saveData.savedGuns.Count; i++)
            {
                //characterSelection.characterList.Add(Resources.Load<GameObject>("Characters/" + saveData.savedSkins[i]));
                GameObject go = Instantiate(Resources.Load<GameObject>("Weapons/" + saveData.savedGuns[i]), GameObject.FindGameObjectWithTag("Weapon").transform);
                go.name = saveData.savedGuns[i];
                //characterSelection.updateList = true;
                Debug.Log("Loaded Gun: " + saveData.savedGuns[i]);
            }

            weaponSelection.updateList = true;


            stream.Close();

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    void WaitForLoadToFinish()
    {
        mainCamera.GetComponent<Animator>().enabled = true;
        loadFinished = true;
    }
}