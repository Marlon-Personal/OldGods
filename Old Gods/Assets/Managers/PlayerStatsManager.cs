using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public LevelDB levelDB;
    public CharacterDB characterDB;
    public SpriteRenderer unitSprite;
    public Text classText;

    public Text characterDesc;
    public Text AttackText;
    public Text DefenseText;
    public Text HPText;
    public Text SpeedText;
    public Text level;
    public Slider xpSlider;
    public Text xpText;
        private Character character;

    //Attacks
    public Text attackName;
    public Text attackDamage;
    public Text attackDescription;

    public Text attackName2;
    public Text attackDamage2;
    public Text attackDescription2;


    public Button UpgradeBTN1;
    public Button UpgradeBTN2;

    public Text attackPoints;

    Character player;
    void Start()
    {
        LoadStatsInGame();

    }

    void LoadStatsInGame()
    {
        player = new Character();
        player.LoadPlayer();
        character = characterDB.GetCharacterByID(player.spriteID);
        unitSprite.sprite = character.spritePortrait;
        level.text = PlayerPrefs.GetInt("lvl").ToString();
        AttackText.text = player.AttackStat.ToString();
        DefenseText.text = player.DefenseStat.ToString();
        SpeedText.text = player.SpeedStat.ToString();
        HPText.text = player.hpStat.ToString();
        classText.text = player.characterClass.ToUpper();

        characterDesc.text = player.characterDesc;
        attackName.text = player.attack1.attackName;
        attackDamage.text = player.attack1.attackDamage.ToString();
        attackName2.text = player.attack2.attackName;
        attackDescription2.text = player.attack2.attackDescription;
        attackDescription.text = player.attack1.attackDescription;
        attackDamage2.text = player.attack2.attackDamage.ToString();

        Level nextLevel = levelDB.GetLevelByLevel(PlayerPrefs.GetInt("lvl") + 1);
        xpText.text = PlayerPrefs.GetInt("xp").ToString() + " / " + nextLevel.xpNeeded.ToString();
        xpSlider.maxValue = nextLevel.xpNeeded;
        xpSlider.value = PlayerPrefs.GetInt("xp");


        if (PlayerPrefs.GetInt("attackPoints") <= 0)
        {
            UpgradeBTN1.interactable = false;
            UpgradeBTN2.interactable = false;
        }

        attackPoints.text = "Available Points: " + PlayerPrefs.GetInt("attackPoints");

    }

    public void GoBack()
    {
        SceneManager.LoadScene("3.PlayerHubScene");
    }

    public void onUpgradeAttack1BTN()
    {
        if (PlayerPrefs.GetInt("attackPoints") > 0)
        {
            player.attack1.attackDamage = player.attack1.attackDamage + 1;
            SaveSystem.SavePlayer(player);
            PlayerPrefs.SetInt("attackPoints", PlayerPrefs.GetInt("attackPoints") - 1);
            SceneManager.LoadScene("3.PlayerStatsScene");
        }
    }

    public void onUpgradeAttack2BTN()
    {
        if (PlayerPrefs.GetInt("attackPoints") > 0)
        {
            player.attack2.attackDamage = player.attack2.attackDamage + 1;
            SaveSystem.SavePlayer(player);
            PlayerPrefs.SetInt("attackPoints", PlayerPrefs.GetInt("attackPoints") - 1);
            SceneManager.LoadScene("3.PlayerStatsScene");
        }
    }
}
