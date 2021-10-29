using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHubSceneManager : MonoBehaviour
{

    public LevelDB levelDB;
    public Animator playerAnimator;

    public Text lvlText;
    public Text xpText;
    public Text characterClass;
    public Text battlesWon;
    public Text battlesLost;
    public Slider xpSlider;


    //  public SpriteRenderer artworkSprite;


    void Start()
    {
        LoadStatsInGame();

    }

    void LoadStatsInGame()
    {
        Character player = new Character();
        player.LoadPlayer();

        lvlText.text = PlayerPrefs.GetInt("lvl").ToString();

        Level nextLevel = levelDB.GetLevelByLevel(PlayerPrefs.GetInt("lvl") + 1);


        xpText.text = PlayerPrefs.GetInt("xp").ToString() + " / " + nextLevel.xpNeeded.ToString();
        characterClass.text = player.characterClass.ToUpper();
        xpSlider.maxValue = nextLevel.xpNeeded;
        xpSlider.value = PlayerPrefs.GetInt("xp");
        battlesWon.text = PlayerPrefs.GetInt("battlesWon").ToString();
        battlesLost.text = PlayerPrefs.GetInt("battlesLost").ToString();

        playerAnimator.SetInteger("CharacterSpriteID", player.spriteID);


    }

    public void GoBack()
    {
        SceneManager.LoadScene("1.MainMenuScene");
    }

    public void Battles()
    {
        SceneManager.LoadScene("4.BattleSelectionScene");
    }

    public void Stats()
    {
        SceneManager.LoadScene("3.PlayerStatsScene");
    }
}
