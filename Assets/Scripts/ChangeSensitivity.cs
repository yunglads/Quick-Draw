using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Text sensValue;

    ECM.Components.MouseLook mouseLook;

    private void Update()
    {
        if (mouseLook == null)
        {
            mouseLook = FindObjectOfType<ECM.Components.MouseLook>();
        }

        sensValue.text = sensitivitySlider.value.ToString();
    }

    public void ApplySensitivity()
    {
        mouseLook.ChangeSensitivity(sensitivitySlider.value, sensitivitySlider.value);
    }
}
