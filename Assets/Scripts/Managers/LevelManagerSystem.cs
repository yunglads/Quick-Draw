using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerSystem : Singleton<LevelManagerSystem>
{
    [SerializeField]
    private Level[] levels;
    [SerializeField]
    private int currentLevel;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private SceneController sceneController;

    public CameraController CameraController;

    [SerializeField]
    private GameStats gameStats;

    protected override void Awake()
    {
        base.Awake();
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

    public void BackToMenu()
    {

    }

    public void LoadMainMenu()
    {
        sceneController.LoadLevelByName("MainMenu");
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

    public void LevelCompleted(float time)
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
            if(gameController == null)
            {
                CameraController.EnableLevelCamera();
                gameController = FindObjectOfType<GameController>();
                gameController.LevelButton();
            }
        }
        else
        {
            CameraController.DisableCameras();
        }
    }
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
    public int reward;
}