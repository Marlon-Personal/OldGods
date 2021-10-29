using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Beast
{
    public int spriteID;
    public Sprite portraitSprite;
    public int xpEarned;
    public string className;
    public string Description;

    public int attack;
    public int defense;
    public int speed;
    public int hp;

    public Attack attack1;
    public Attack attack2;

}