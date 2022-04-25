using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAudio : MonoBehaviour
{
    AudioSource shopSFX;
    public AudioClip buy1;
    public AudioClip buy2;

    // Update is called once per frame
    void Update()
    {
        if (shopSFX == null)
        {
            shopSFX = FindObjectOfType<AudioSource>();
        }
    }

    public void PlayRandomShopAudio()
    {
        int randomAudio;
        randomAudio = Random.Range(0, 2);
        if (randomAudio == 0)
        {
            shopSFX.PlayOneShot(buy1, .5f);
            Debug.Log("Playing audio 1");
        }
        else if (randomAudio == 1)
        {
            shopSFX.PlayOneShot(buy2, .5f);
            Debug.Log("Playing audio 2");
        }
    }
}
