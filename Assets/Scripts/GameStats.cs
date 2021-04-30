using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStats : MonoBehaviour
{
    public int totalStars = 0;
    public float playerMoney = 0;
    public int playerGold = 0;

    public int levelReward;
    public bool statUpdate = false;
    bool foundUI = false;
    //bool statsSet = false;

    Scene scene;
    int index = 0;

    public Text playerMoneyText;
    public Text playerGoldText;
    public Text totalStarsText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == index && !foundUI)
        {
            //levelReward = 0;
            playerMoneyText = GameObject.FindGameObjectWithTag("MoneyUI").GetComponent<Text>();
            playerGoldText = GameObject.FindGameObjectWithTag("GoldUI").GetComponent<Text>();
            totalStarsText = GameObject.FindGameObjectWithTag("StarsUI").GetComponent<Text>();
            foundUI = true;
            Debug.Log("UI found");
        }
        else if (scene.buildIndex != index)
        {
            foundUI = false;
        }

        if (scene.buildIndex == index)
        {
            playerMoneyText.text = playerMoney.ToString("F2");
            playerGoldText.text = playerGold.ToString();
            totalStarsText.text = totalStars.ToString();
        }
    }
}
