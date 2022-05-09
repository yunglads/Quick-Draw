using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSFX : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void OnClickSFX()
    {
        source.PlayOneShot(clip, 1f);
    }
}
