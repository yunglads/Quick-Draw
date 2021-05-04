using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerSystem : MonoBehaviour
{
    public static LevelManagerSystem Instance { get; private set; }

    [SerializeField] GameObject levelsContainer;
    [SerializeField] LevelSelect[] levels;
    [SerializeField] int currentLevel;
    [SerializeField] GameController gameController;
    [SerializeField] SceneController sceneController;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;    
        levels = levelsContainer.gameObject.GetComponentsInChildren<LevelSelect>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        //gameController.PlayOpening();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        if (!IsLastLevel())
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

    public bool IsLastLevel()
    {
        return currentLevel >= levels.Length;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            if(gameController == null)
            {
                gameController = FindObjectOfType<GameController>();
                gameController.LevelButton();
            }
        }
    }
}