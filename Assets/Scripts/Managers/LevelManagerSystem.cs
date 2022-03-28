using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelManagerSystem : Singleton<LevelManagerSystem>
{
    public Level[] levels;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private SceneController sceneController;

    private LoadMenuMode loadMenuMode;

    public CameraController CameraController;

    [SerializeField]
    private GameStats gameStats;

    protected override void Awake()
    {
        base.Awake();
        loadMenuMode = LoadMenuMode.MainMenu;
        CheckLevelsLocked();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FillLevelsUI(LevelUI[] levelsUI)
    {
        for (int i = 0; i < levelsUI.Length; i++)
        {
            levelsUI[i].SetLevel(levels[i]);
        }
    }

    public void SetCurrentLevel(int newLevel)
    {
        currentLevel = newLevel;
    }

    public Level GetCurrentLevel()
    {
        return levels[currentLevel-1];
    }

    public void PlayLevel()
    {
        sceneController.LoadLevelByInt(currentLevel);
    }

    public void NextLevel()
    {
        if (IsNextLevelAvailable())
        {
            currentLevel++;
            PlayLevel();
        }
    }

    public void RetryLevel()
    {
        PlayLevel();
    }

    public void LoadMainMenu()
    {
        sceneController.LoadLevelByName("MainMenu");
        loadMenuMode = LoadMenuMode.MainMenu;
    }

    public void LoadLevelSelection()
    {
        sceneController.LoadLevelByName("MainMenu");
        loadMenuMode = LoadMenuMode.LevelSelection;
    }

    public bool IsNextLevelAvailable()
    {
        bool isAvailable = currentLevel < levels.Length;
        if (isAvailable)
        {
            isAvailable = !levels[currentLevel].isLocked;
        }
        return isAvailable;
    }

    public void SetLevelStars(int levelStars)
    {
        GetCurrentLevel().stars = levelStars;
    }

    public void CompleteLevel(float time)
    {
        if(levels[currentLevel - 1].completionTime == 0 || levels[currentLevel - 1].completionTime > time)
        {
            Debug.Log("New Best Time");
            levels[currentLevel - 1].completionTime = time;
        }
        levels[currentLevel - 1].levelCompleted = true;
        CheckLevelsLocked();
    }

    private void CheckLevelsLocked()
    {
        for(int i=currentLevel; i<levels.Length; i++)
        {
            if (levels[i].starsNeeded <= gameStats.totalStars)
            {
                levels[i].isLocked = false;
            }
            else
            {
                levels[i].isLocked = true;
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            gameController = FindObjectOfType<GameController>();
            switch (loadMenuMode)
            {
                case LoadMenuMode.LevelSelection:
                    CameraController.EnableLevelCamera();
                    gameController.LevelButton();
                    break;
                case LoadMenuMode.MainMenu:
                    CameraController.EnableMenuCamera();
                    break;
            }
        }
        else
        {
            int levelNumber = int.Parse(scene.name.Substring(5));
            SetCurrentLevel(levelNumber);
            CameraController.DisableCameras();
        }
    }

    //public void SaveLevelData()
    //{
    //    Level data = new Level();

    //    for (int i = 0; i < levels.Length; i++)
    //    {
    //        data.levelID = levels[i].levelID;
    //        data.levelCompleted = levels[i].levelCompleted;
    //        data.stars = levels[i].stars;
    //        data.starsNeeded = levels[i].starsNeeded;
    //        data.completionTime = levels[i].completionTime;
    //        data.isLocked = levels[i].isLocked;
    //        data.enemyName = levels[i].enemyName;
    //        data.levelReward = levels[i].levelReward;
    //        data.twoStarTime = levels[i].twoStarTime;
    //    }

    //    SaveSystem.SaveLevelData(data);
    //}

    //public void LoadLevelData()
    //{
    //    string path = Application.persistentDataPath + "/player.save";
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = File.Open(path, FileMode.Open);

    //        Level saveData = (Level)formatter.Deserialize(stream);

    //        for (int i = 0; i < levels.Length; i++)
    //        {
    //            levels[i].levelID = saveData.levelID;
    //            levels[i].levelCompleted = saveData.levelCompleted;
    //            levels[i].stars = saveData.stars;
    //            levels[i].starsNeeded = saveData.starsNeeded;
    //            levels[i].completionTime = saveData.completionTime;
    //            levels[i].isLocked = saveData.isLocked;
    //            levels[i].enemyName = saveData.enemyName;
    //            levels[i].levelReward = saveData.levelReward;
    //            levels[i].twoStarTime = saveData.twoStarTime;
    //        }

    //        stream.Close();

    //        Debug.Log("Game Loaded!");
    //    }
    //    else
    //    {
    //        Debug.LogError("Save file not found in " + path);
    //    }
    //}
}

[System.Serializable]
public class Level
{
    public int levelID;
    public bool levelCompleted;
    public int stars;
    public int starsNeeded;
    public float completionTime;
    public bool isLocked;
    public string enemyName;
    public int levelReward;
    public float twoStarTime;
}

public enum LoadMenuMode
{
    LevelSelection,
    MainMenu
}