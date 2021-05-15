using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM.Components;

public class Player : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject deathPanel;
    public GameObject winPanel;
    public GameObject playerWeapon;
    public GameObject playerModel;
    public GameObject ECM_FirstPerson;

    public bool isDead = false;
    private float winDelayTime = 3;

    void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (playerModel == null)
        {
            playerModel = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void PlayerDead()
    {
        playerWeapon.GetComponent<Rigidbody>().useGravity = true;
        playerWeapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        playerCamera.GetComponent<Animator>().enabled = true;
        deathPanel.SetActive(true);
        deathPanel.GetComponent<Animator>().enabled = true;
        playerModel.SetActive(false);
        ECM_FirstPerson.GetComponent<MouseLook>().lateralSensitivity = 0;
        ECM_FirstPerson.GetComponent<MouseLook>().verticalSensitivity = 0;
        ECM_FirstPerson.GetComponent<MouseLook>().lockCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Victory()
    {
        Invoke("PlayerWon", winDelayTime);
    }

    void PlayerWon()
    {
        ECM_FirstPerson.GetComponent<MouseLook>().lateralSensitivity = 0;
        ECM_FirstPerson.GetComponent<MouseLook>().verticalSensitivity = 0;
        ECM_FirstPerson.GetComponent<MouseLook>().lockCursor = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SetLevelResults();
        winPanel.SetActive(true);
    }

    void SetLevelResults()
    {
        float levelTimer = FightController.Instance.gameTimer + FightController.Instance.additionalTimer - winDelayTime;

        int levelStars = 1;
         if (levelTimer <= LevelManagerSystem.Instance.GetCurrentLevel().twoStarTime)
         {
             levelStars = 2;
         }

         GameStats.Instance.totalStars += levelStars;
         GameStats.Instance.playerMoney += GameStats.Instance.levelReward;
         //GameStats.Instance.levelReward = 0;

         LevelManagerSystem.Instance.LevelCompleted(levelTimer);
    }
}
