using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : ButtonController
{
    protected override void OnButtonClicked()
    {
        LevelManagerSystem.Instance.NextLevel();
    }

    private void OnEnable()
    {
        if (LevelManagerSystem.Instance.IsLastLevel())
        {
            gameObject.SetActive(false);
        }
    }
}