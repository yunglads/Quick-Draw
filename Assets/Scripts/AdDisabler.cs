using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdDisabler : MonoBehaviour
{
    public GameObject ad;
    public GameObject adManager;
    public GameObject removeAdsButton;

    public int disableAds;
    public bool adsDisabled = false;
    bool runOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        //Use below code to reset index if index gets stuck on "out of range"
        PlayerPrefs.SetInt("disableAds", 0);

        if (!PlayerPrefs.HasKey("disableAds"))
        {
            PlayerPrefs.SetInt("disableAds", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (adsDisabled)
        {
            disableAds = 1;
            Save();
            removeAdsButton = GameObject.Find("Remove Ads");
            Destroy(removeAdsButton);
        }

        if (adsDisabled && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            ad = GameObject.Find("Interstatial Ad Button");
            adManager = GameObject.Find("AdManager");
            Destroy(adManager);
            Destroy(ad);
            runOnce = true;
        }

        if (disableAds == 1)
        {
            adsDisabled = true;
        }

        //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        //{
        //    runOnce = false;
        //}
    }

    public void Load()
    {
        disableAds = PlayerPrefs.GetInt("disableAds");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("disableAds", disableAds);
    }
}
