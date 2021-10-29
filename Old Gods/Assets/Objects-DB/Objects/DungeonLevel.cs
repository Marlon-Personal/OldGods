using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonLevel
{
    public int level;
    public int numberOfBattles;
    public string dungeonName;
    public string dungeonDescription;
    public Beast[] battle;
}
