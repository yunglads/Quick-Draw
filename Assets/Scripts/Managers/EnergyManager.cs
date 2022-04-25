using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnergyManager : MonoBehaviour
{
    public int maxEnergy = 10;
    public int currentEnergy;
    int restoreDuration = 10;

    public Text energyTimerText;
    public Text energyCountText;

    public DateTime nextEnergyTime;
    public DateTime lastEnergyTime;

    bool isRestoring = false;
    public bool runOnce = false;
    bool onMainMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        //Use below code to reset index if index gets stuck on "out of range"
        PlayerPrefs.SetInt("currentEnergy", maxEnergy);

        if (!PlayerPrefs.HasKey("currentEnergy"))
        {
            PlayerPrefs.SetInt("currentEnergy", maxEnergy);
            Load();
            StartCoroutine(RestoreEnergy());
        }
        else
        {
            Load();
            StartCoroutine(RestoreEnergy());
        }
    }

    public IEnumerator RestoreEnergy()
    {
        UpdateEnergy();
        UpdateTimer();
        isRestoring = true;

        while(currentEnergy < maxEnergy)
        {
            DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
            DateTime nextDateTime = nextEnergyTime;
            bool isEnergyAdding = false;

            while(currentDateTime > nextDateTime)
            {
                if (currentEnergy < maxEnergy)
                {
                    isEnergyAdding = true;
                    currentEnergy++;
                    UpdateEnergy();
                    DateTime timeToAdd = lastEnergyTime > nextDateTime ? lastEnergyTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd, restoreDuration);
                }
                else
                {
                    break;
                }
            }

            if (isEnergyAdding)
            {
                lastEnergyTime = WorldTimeAPI.Instance.GetCurrentDateTime();
                nextEnergyTime = nextDateTime;
            }

            UpdateTimer();
            UpdateEnergy();
            Save();
            yield return null;
        }

        isRestoring = false;
    }

    public void UseEnergy()
    {
        if (currentEnergy >= 1)
        {
            currentEnergy--;
            //UpdateEnergy();

            if (!isRestoring)
            {
                if (currentEnergy + 1 == maxEnergy)
                {
                    nextEnergyTime = AddDuration(WorldTimeAPI.Instance.GetCurrentDateTime(), restoreDuration);
                }

                StartCoroutine(RestoreEnergy());
            }
        }
        else
        {
            Debug.Log("Insufficient energy");
        }
    }

    public void Load()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");
        nextEnergyTime = StringToDate(PlayerPrefs.GetString("nextEnergyTime"));
        lastEnergyTime = StringToDate(PlayerPrefs.GetString("lastEnergyTime"));
    }

    public void Save()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        PlayerPrefs.SetString("nextEnergyTime", nextEnergyTime.ToString());
        PlayerPrefs.SetString("lastEnergyTime", lastEnergyTime.ToString());
    }

    DateTime StringToDate(string datetime)
    {
        if (string.IsNullOrEmpty(datetime))
        {
            return WorldTimeAPI.Instance.GetCurrentDateTime();
        }
        else
        {
            return DateTime.Parse(datetime);
        }
    }

    DateTime AddDuration(DateTime datetime, int duration)
    {
        return datetime.AddMinutes(duration);
    }

    public void UpdateEnergy()
    {
        energyCountText.text = currentEnergy.ToString();
    }

    public void UpdateTimer()
    {
        if (currentEnergy >= maxEnergy)
        {
            energyTimerText.text = "Full";
            return;
        }

        TimeSpan time = nextEnergyTime - WorldTimeAPI.Instance.GetCurrentDateTime();
        string value = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
        energyTimerText.text = value;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            onMainMenu = true;
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            onMainMenu = false;
            //Debug.Log("new scene loaded");
        }

        if (!onMainMenu)
        {
            runOnce = false;
        }

        if (onMainMenu)
        {
            if (FindObjectOfType<GameController>().screenClosed && !runOnce)
            {
                StartCoroutine(RestoreEnergy());
                runOnce = true;
            }
        }

        //Debug.Log(nextEnergyTime);
        //Debug.Log(lastEnergyTime);
        if (energyCountText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            energyCountText = GameObject.Find("Energy Count").GetComponent<Text>();
        }

        if (energyTimerText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            energyTimerText = GameObject.Find("Timer").GetComponent<Text>();
        }


        //if (energyTimerStarted && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        //{
        //    energyTimerText.enabled = true;
        //    energyTimerText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        //}

        //if (!energyTimerStarted && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu")) 
        //{
        //    energyTimerText.enabled = false;
        //}
    }
}
