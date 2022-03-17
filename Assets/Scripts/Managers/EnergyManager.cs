using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    public int maxEnergy = 10;
    public int currentEnergy;

    //public int minutes;
    //public int seconds;

    public Text energyTimerText;
    public Text energyCountText;

    TimeSpan time;

    DateTime currentDateTime;
    DateTime energyTimer;

    bool timeSet = false;
    bool energyTimerStarted = false;

    // Start is called before the first frame update
    void Start()
    {

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

        energyCountText.text = currentEnergy.ToString();

        if (currentEnergy < maxEnergy && !timeSet)
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

    }

    void SetEnergyTimer()
    {
        currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();
        Debug.Log(currentDateTime);

        energyTimer = currentDateTime.AddMinutes(10);
        Debug.Log(energyTimer);

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
}
