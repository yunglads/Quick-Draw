using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int[] levelID;
    public bool[] levelCompleted;
    public int[] stars;
    public int[] starsNeeded;
    public float[] completionTime;
    public bool[] isLocked;
    public string[] enemyName;
    public int[] levelReward;
    public float[] twoStarTime;
}
