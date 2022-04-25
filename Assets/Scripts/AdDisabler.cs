using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdDisabler : MonoBehaviour
{
    public GameObject ad;
    public GameObject adManager;
    public GameObject removeAdsButton;

    public bool adsDisabled = false;
    bool runOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (adsDisabled)
        {
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

        //if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        //{
        //    runOnce = false;
        //}
    }
}
