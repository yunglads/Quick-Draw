using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    string sceneName;
    int buildIndex;
    public int LevelID;

    //LevelSelect[] levelSelect;

    private void Awake()
    {
        //levelSelect = FindObjectsOfType<LevelSelect>();
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelByInt()
    {
        SceneManager.LoadScene(LevelID);
    }

    public void LoadNextLevel()
    {
        //buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(buildIndex + 1);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(buildIndex);
    }
}
