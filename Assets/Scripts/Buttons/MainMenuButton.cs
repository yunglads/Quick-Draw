using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : ButtonController
{
    protected override void OnButtonClicked()
    {
        LevelManagerSystem.Instance.LoadMainMenu();
    }
}
