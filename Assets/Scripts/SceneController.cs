using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int buildIndex;

    private void Awake()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelByInt(int levelID)
    {
        SceneManager.LoadScene(levelID);
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
