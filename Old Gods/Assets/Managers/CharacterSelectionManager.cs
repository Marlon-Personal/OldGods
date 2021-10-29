using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterSelectionManager : MonoBehaviour
{
    public CharacterDB characterDB;

    public SpriteRenderer unitSprite;
    public Text characterClass;
    public Text characterDesc;
    public Animator playerAnimator;

    public Text AttackText;
    public Text DefenseText;
    public Text HPText;
    public Text SpeedText;

    //Attacks
    public Text attackName;
    public Text attackDamage;
    public Text attackDesc;

    public Text attackName2;
    public Text attackDamage2;
    public Text attackDesc2;

    private Character character;

    private int selectedOption = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= characterDB.UnitCount)
        {
            selectedOption = 0;
        }


        UpdateCharacter(selectedOption);
    }


    public void UpdateCharacter(int selectedOption)
    {
        character = characterDB.GetCharacter(selectedOption);
        unitSprite.sprite = character.spritePortrait;
        characterClass.text = character.characterClass.ToUpper();
        characterDesc.text = character.characterDesc;
        AttackText.text = character.AttackStat.ToString();
        DefenseText.text = character.DefenseStat.ToString();
        SpeedText.text = character.SpeedStat.ToString();
        HPText.text = character.hpStat.ToString();

        attackName.text = character.attack1.attackName.ToUpper();
        attackDamage.text = character.attack1.attackDamage.ToString();
        attackName2.text = character.attack2.attackName.ToUpper();
        attackDesc2.text = character.attack2.attackDescription;
        attackDesc.text = character.attack1.attackDescription;
        attackDamage2.text = character.attack2.attackDamage.ToString();

        playerAnimator.SetInteger("CharacterSpriteID", character.spriteID);
    }

    public void PrevOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterDB.UnitCount - 1;
        }


        UpdateCharacter(selectedOption);
    }


    public void BackOption()
    {
        SceneManager.LoadScene("1.MainMenuScene");
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("xp", 0);
        PlayerPrefs.SetInt("lvl", 1);
        PlayerPrefs.SetInt("battlesWon", 0);
        PlayerPrefs.SetInt("battlesLost", 0);
        PlayerPrefs.SetInt("dungeonLevelPassed", 0);
        PlayerPrefs.SetInt("attackPoints", 0);
        SaveSystem.SavePlayer(character);
        SceneManager.LoadScene("3.PlayerHubScene");
    }
}