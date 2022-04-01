using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int savedTotalStars;
    public float savedPlayerMoney;
    public int savedPlayerGold;
    public int savedEnergy;
    public long savedExitTimer;
    public Level[] savedLevels;
    public List<string> savedSkins;
    public List<string> savedGuns;
}
