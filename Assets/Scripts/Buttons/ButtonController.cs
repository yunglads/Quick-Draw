using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonController : MonoBehaviour
{
    private Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.Log("Button component is missing! game object: " + gameObject.name);
        }
    }

    protected abstract void OnButtonClicked();
}