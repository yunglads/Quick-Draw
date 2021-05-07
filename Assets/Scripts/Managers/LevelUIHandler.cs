using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIHandler : Singleton<LevelUIHandler>
{
    [SerializeField] LevelUI[] levelsUI;

    protected override void Awake()
    {
        levelsUI = GetComponentsInChildren<LevelUI>();
    }

    private void Start()
    {
        LevelManagerSystem.Instance.FillLevelsUI(levelsUI);
    }
}
