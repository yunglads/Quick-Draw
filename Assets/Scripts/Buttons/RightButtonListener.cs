using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightButtonListener : MonoBehaviour
{
    Button button;

    CharacterSelection characterSelection;

    // Start is called before the first frame update
    void Start()
    {
        characterSelection = FindObjectOfType<CharacterSelection>();

        button = GetComponent<Button>();
        button.onClick.AddListener(characterSelection.RightButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterSelection == null)
        {
            characterSelection = FindObjectOfType<CharacterSelection>();
        }
    }
}
