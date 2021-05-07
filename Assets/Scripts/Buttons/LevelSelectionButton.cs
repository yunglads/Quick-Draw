using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : ButtonController
{
    protected override void OnButtonClicked()
    {
        LevelManagerSystem.Instance.LoadLevelSelection();
    }
}
