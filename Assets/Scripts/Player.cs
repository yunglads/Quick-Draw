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
        playerWeapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    void Update()
    {
        if (playerModel == null)
        {
            playerModel = GameObject.FindGameObjectWithTag("Player");
        }

        if (playerWeapon == null)
        {
            playerWeapon = GameObject.FindGameObjectWithTag("Weapon");
        }
    }

    public void PlayerDead()
    {
        //playerWeapon.GetComponentInChildren<Animator>().enabled = false;
        //playerWeapon.GetComponentInChildren<Rigidbody>().useGravity = true;
        //playerWeapon.GetComponentInChildren<Rigidbody>().isKinematic = false;
        //playerWeapon.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
        playerCamera.GetComponent<Animator>().enabled = true;
        deathPanel.SetActive(true);
        deathPanel.GetComponent<Animator>().enabled = true;
        playerModel.SetActive(false);
        ECM_FirstPerson.GetComponent<MouseLook>().lateralSensitivity = 0;
        ECM_FirstPerson.GetComponent<MouseLook>().verticalSensitivity = 0;
        //ECM_FirstPerson.GetComponent<MouseLook>().lockCursor = false;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
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

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;

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
        
        if (!LevelManagerSystem.Instance.GetCurrentLevel().levelCompleted)
        {
            GameStats.Instance.UpdateStars(levelStars);
            GameStats.Instance.UpdateMoney(LevelManagerSystem.Instance.GetCurrentLevel().levelReward);
        }

        if (LevelManagerSystem.Instance.GetCurrentLevel().levelCompleted && levelStars > LevelManagerSystem.Instance.GetCurrentLevel().stars)
        {
            GameStats.Instance.UpdateStars(levelStars - 1);
        }

        LevelManagerSystem.Instance.SetLevelStars(levelStars);

        LevelManagerSystem.Instance.CompleteLevel(levelTimer);
    }
}
