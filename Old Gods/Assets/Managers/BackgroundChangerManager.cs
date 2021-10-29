using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChangerManager : MonoBehaviour
{

    public BackgroundDB backgroundDB;

    public SpriteRenderer backgroundSprite;
    public SpriteRenderer landtile1;
    public SpriteRenderer landtile2;
    public SpriteRenderer landtile3;
    public SpriteRenderer landtile4;
    public SpriteRenderer landtile5;
    public SpriteRenderer landtile6;
    public SpriteRenderer landtile7;
    public SpriteRenderer landtile8;
    public SpriteRenderer landtile9;
    public SpriteRenderer landtile10;
    public SpriteRenderer landtile11;
    public SpriteRenderer landtile12;
    public SpriteRenderer landtile13;
    public SpriteRenderer landtile14;


    private DungeonLevel dungeonLevel;
    // Start is called before the first frame update
    void Start()
    {
        AddBackground(PlayerPrefs.GetInt("dungeonLevel"));
    }


    public void AddBackground(int currentLevel)
    {

      Background  background = backgroundDB.GetBackgroundByLevel(currentLevel);

        backgroundSprite.sprite = background.background;
        landtile1.sprite = background.tileLeft;
        landtile2.sprite = background.tileCenter;
        landtile3.sprite = background.tileCenter;
        landtile4.sprite = background.tileCenter;
        landtile5.sprite = background.tileCenter;
        landtile6.sprite = background.tileCenter;
        landtile7.sprite = background.tileCenter;
        landtile8.sprite = background.tileCenter;
        landtile9.sprite = background.tileCenter;
        landtile10.sprite = background.tileCenter;
        landtile11.sprite = background.tileCenter;
        landtile12.sprite = background.tileCenter;
        landtile13.sprite = background.tileCenter;
        landtile14.sprite = background.tileRight;




    }
}
