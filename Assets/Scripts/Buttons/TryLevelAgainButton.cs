using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryLevelAgainButton : ButtonController
{
    protected override void OnButtonClicked()
    {
        LevelManagerSystem.Instance.RetryLevel();
    }
}