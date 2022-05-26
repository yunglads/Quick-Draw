﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ECM.Components;

public class FightController : Singleton<FightController>
{
    public float additionalTimer;
    float drawTimer;
    public float gameTimer;
    public float gameTimerSet;

    public int levelStars;
    public int levelReward;

    GameObject player;
    EnemyAI[] enemyAI;
    WeaponController[] weaponControllers;

    public GameObject ecmFP;
    public GameObject introCanvas;

    public Button drawButton;
    public Text drawTimerText;

    float randomXPos;
    float randomYpos;

    public float noAmmoTimer;

    public bool noAmmo = false;

    bool startCounters = false;
    bool fightStarted = false;
    bool timerSet = false;
    bool audioPlayed = false;

    public GameObject mobileUI;
    public GameObject tutorialPanel;
    public GameObject weapon;

    public GameObject introSong;

    public Camera playerCam;
    public Camera enemyCam;
    public GameObject[] lookPoints;
    int rngLookPoint;
    int index = 0;

    Player playerController;
    public int enemiesNumber;

    public AudioSource source;
    public AudioClip draw1;
    public AudioClip draw2;

    protected override void Awake()
    {
        base.Awake();
        additionalTimer = 0;
        enemiesNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        playerController = FindObjectOfType<Player>();
    }

    void Start()
    {
        enemyAI = FindObjectsOfType<EnemyAI>();
        weaponControllers = FindObjectsOfType<WeaponController>();
        player = GameObject.FindGameObjectWithTag("PlayerController");
        player.SetActive(false);
        weapon = GameObject.FindGameObjectWithTag("Weapon");

        //player.GetComponentInChildren<MouseLook>().lockCursor = false;
        //player.GetComponentInChildren<MouseLook>().lateralSensitivity = 0;
        //player.GetComponentInChildren<MouseLook>().verticalSensitivity = 0;
        //player.GetComponentInChildren<WeaponController>().enabled = false;

        randomXPos = Random.Range(0, 600);
        randomYpos = Random.Range(0, 250);

        
        //drawButton.interactable = false;
    }

    void Update()
    {
        //EmergencyKillPlayer();

        if (weapon == null)
        {
            weapon = GameObject.FindGameObjectWithTag("Weapon");
        }

        if (startCounters)
        {
            drawButton.transform.position = new Vector3(randomXPos, randomYpos, 0);
            drawButton.gameObject.SetActive(false);
        }

        if (!fightStarted && startCounters)
        {
            drawTimer += Time.deltaTime;
            drawTimerText.text = drawTimer.ToString("F2");

            if (drawTimer >= 3f)
            {
                foreach (EnemyAI enemy in enemyAI)
                {
                    enemy.gunIsDrawn = true;
                }

                int randomAudio;
                randomAudio = Random.Range(0, 2);

                if (randomAudio == 0 && !audioPlayed)
                {
                    source.PlayOneShot(draw1, 1f);
                    audioPlayed = true;
                }
                else if (randomAudio == 1 && !audioPlayed)
                {
                    source.PlayOneShot(draw2, 1f);
                    audioPlayed = true;
                }

                drawTimerText.text = "Draw!!!";

                drawButton.gameObject.SetActive(true);
                //drawButton.interactable = true;

                additionalTimer += Time.deltaTime;
            }
        }

        if (fightStarted)
        {
            gameTimer += Time.deltaTime;

            if (AllEnemiesDead() && !timerSet)
            {
                gameTimerSet = gameTimer;
                                              
                timerSet = true;
            }
        }

        if (weapon.GetComponentInChildren<WeaponController>().ammoCount <= 0)
        {
            noAmmo = true;
        }

        if (noAmmo)
        {
            noAmmoTimer += Time.deltaTime;
        }

        if (noAmmoTimer >= 5f)
        {
            EmergencyKillPlayer();
        }
    }

    public void DrawButton()
    {
        fightStarted = true;
        audioPlayed = false;
        drawTimer = 0;
        drawButton.gameObject.SetActive(false);
        drawTimerText.gameObject.SetActive(false);
        weapon.GetComponentInChildren<Animator>().SetTrigger("gunDrawn");
        //player.GetComponentInChildren<MouseLook>().lockCursor = true;
        //player.GetComponentInChildren<MouseLook>().lateralSensitivity = 2;
        //player.GetComponentInChildren<MouseLook>().verticalSensitivity = 2;
        weapon.GetComponentInChildren<WeaponController>().enabled = true;
        playerCam.transform.LookAt(lookPoints[RandomLookPoint(index)].gameObject.transform.position);
        Vector3 eulerRotation = playerCam.transform.rotation.eulerAngles;
        playerCam.transform.rotation = Quaternion.Euler(eulerRotation.x, 90, eulerRotation.z);
        ecmFP.transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);

#if UNITY_IOS
        mobileUI.SetActive(true);
#endif

#if UNITY_ANDROID
        mobileUI.SetActive(true);
#endif

#if UNITY_STANDALONE_WIN
        Cursor.visible = false;
#endif
    }

    //selects a random lookpoint from the object list "LookPoints" in editor
    public int RandomLookPoint(int index)
    {
        rngLookPoint = Random.Range(0, lookPoints.Length);
        index = rngLookPoint;
        return index;
    }

    public void KillEnemy()
    {
        enemiesNumber--;
        if (AllEnemiesDead())
        {
            foreach (WeaponController weapon in weaponControllers)
            {
                weapon.ResetAmmoCount();
            }

            playerController.Victory();
            //weapon.transform.SetParent(null);
            Invoke("ResetGunPos", 3f);
            mobileUI.SetActive(false);
        }
    }

    public bool AllEnemiesDead()
    {
        return enemiesNumber <= 0;
    }

    public void KillPlayer()
    {
        playerController.PlayerDead();
        weapon.transform.SetParent(null);
        mobileUI.SetActive(false);
    }

    public void StartFightButton()
    {
        startCounters = true;
        introCanvas.SetActive(false);
        enemyCam.enabled = false;
        player.SetActive(true);
        Destroy(introSong);
    }

    public void ContinueTutorialButton()
    {
        tutorialPanel.SetActive(false);
    }

    void ResetGunPos()
    {
        weapon.GetComponentInChildren<Animator>().SetTrigger("resetGun");
    }

    void EmergencyKillPlayer()
    {
        weapon.GetComponentInChildren<WeaponController>().ammoCount = weapon.GetComponentInChildren<WeaponController>().resetAmmoCount - 1;
        noAmmo = false;
        noAmmoTimer = 0;
        KillPlayer();
    }
}
