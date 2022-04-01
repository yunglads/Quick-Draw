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

    //public int minutes;
    //public int seconds;

    public Text energyTimerText;
    public Text energyCountText;

    TimeSpan time;

    public DateTime currentDateTime;
    public DateTime energyTimer;
    public DateTime oldTimer;

    bool timeSet = false;
    bool energyTimerStarted = false;

    bool loadFinished = false;
    bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();

        //Invoke("OnStartTimerCheck", 4f);

        //for (int i = 0; i < maxEnergy; i++)
        //{
        //    if (currentDateTime >= onExitTimer && currentEnergy < maxEnergy)
        //    {
        //        currentEnergy++;
        //        onExitTimer.AddMinutes(10);
        //        Debug.Log(currentDateTime);
        //        Debug.Log(onExitTimer);
        //    }
        //    else
        //    {
        //        loadFinished = true;
        //        break;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (energyTimerText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            energyTimerText = GameObject.Find("Timer").GetComponent<Text>();
        }

        if (energyCountText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            energyCountText = GameObject.Find("Energy Count").GetComponent<Text>();
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            energyCountText.text = currentEnergy.ToString();
        }

        if (currentEnergy < maxEnergy && !loadFinished)
        {
            OnStartTimerCheck();
            reset = true;
        }

        if (currentEnergy < maxEnergy && !timeSet && loadFinished)
        {
            SetEnergyTimer();
        }

        if (timeSet)
        {
            StartCountdown();
            AddEnergy();
        }

        if (energyTimerStarted && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            energyTimerText.enabled = true;
            energyTimerText.text = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        }

        if (!energyTimerStarted && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu")) 
        {
            energyTimerText.enabled = false;
        }

        if (currentEnergy >= maxEnergy)
        {
            timeSet = false;
            energyTimerStarted = false;
        }

    }

    void SetEnergyTimer()
    {
        currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
        //Debug.Log(currentDateTime);

        if (energyTimer < oldTimer)
        {
            energyTimer = oldTimer;
        }
        else
        {
            energyTimer = currentDateTime.AddMinutes(10);
        }

        //if (energyTimer < currentDateTime)
        //{
        //    energyTimer = currentDateTime.AddMinutes(10);
        //}

        timeSet = true;
    }

    void StartCountdown()
    {
        energyTimerStarted = true;

        time = energyTimer - WorldTimeAPI.Instance.GetCurrentDateTime();
    }

    void AddEnergy()
    {
        if (WorldTimeAPI.Instance.GetCurrentDateTime() >= energyTimer)
        {
            currentEnergy++;
            timeSet = false;
            energyTimerStarted = false;
            Debug.Log("energy added checking for new energy");
        }
    }

    private void OnApplicationQuit()
    {
        //asdf = WorldTimeAPI.Instance.GetCurrentDateTime();
    }

    void OnStartTimerCheck()
    {
        if (currentDateTime >= oldTimer && reset)
        {
            currentEnergy++;
            oldTimer.AddMinutes(10);
            reset = false;
        }
        else
        {
            loadFinished = true;
        }
    }
}
