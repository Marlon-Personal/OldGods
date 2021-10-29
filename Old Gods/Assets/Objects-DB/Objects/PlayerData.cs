using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int spriteID;
    public string characterClass;
    public string characterDesc;
    public int hpStat;
    public int AttackStat;
    public int DefenseStat;
    public int SpeedStat;

    public string Attack1;
    public int AttackDamage1;
    public string AttackDescription1;


    public string Attack2;
    public int AttackDamage2;
    public string AttackDescription2;


    public PlayerData(Character player)
    {
        spriteID = player.spriteID;
        characterClass = player.characterClass;
        characterDesc = player.characterDesc;
        hpStat = player.hpStat;
        AttackStat = player.AttackStat;
        DefenseStat = player.DefenseStat;
        SpeedStat = player.SpeedStat;
        Attack1 = player.attack1.attackName;
        AttackDamage1 = player.attack1.attackDamage;
        Attack2 = player.attack2.attackName;
        AttackDamage2 = player.attack2.attackDamage;
        AttackDescription2 = player.attack2.attackDescription;

        AttackDescription1 = player.attack1.attackDescription;
 
    }
}
