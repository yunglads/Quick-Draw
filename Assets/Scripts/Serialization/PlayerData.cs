using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int savedTotalStars;
    public float savedPlayerMoney;
    public int savedPlayerGold;
    public Level[] savedLevels;
    public List<string> savedSkins;
    public string[] savedGuns;
}
