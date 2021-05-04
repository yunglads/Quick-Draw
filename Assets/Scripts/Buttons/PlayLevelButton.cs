using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelButton : ButtonController
{
    protected override void OnButtonClicked()
    {
        LevelManagerSystem.Instance.PlayLevel();
    }
}