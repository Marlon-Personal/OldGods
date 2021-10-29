using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public int spriteID;
    public Sprite spritePortrait;
    public string characterClass;

    public string characterDesc;
    public int hpStat;
    public int AttackStat;
    public int DefenseStat;
    public int SpeedStat;
    public Attack attack1;
    public Attack attack2;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {

        PlayerData data = SaveSystem.LoadPlayer();

        spriteID = data.spriteID;
        characterClass = data.characterClass;
        characterDesc = data.characterDesc;
        AttackStat = data.AttackStat;
        DefenseStat = data.DefenseStat;
        SpeedStat = data.SpeedStat;
        hpStat = data.hpStat;
        attack1 = new Attack();
        attack2 = new Attack();
        attack1.attackName = data.Attack1;
        attack1.attackDamage = data.AttackDamage1;
        attack2.attackName = data.Attack2;
        attack2.attackDamage = data.AttackDamage2;
        attack2.attackDescription = data.AttackDescription2;
        attack1.attackDescription = data.AttackDescription1;

    }
}
