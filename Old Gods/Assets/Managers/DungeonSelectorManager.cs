using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DungeonSelectorManager : MonoBehaviour
{
    public DungeonDB dungeonDB;

    public Text dungeonNameText;
    public Text dungeonDescText;
    public Text xpEarned;
    public Text numberBattles;
    public Text dungeonCompleted;

    private DungeonLevel dungeonLevel;

    private int idBattle;

    private int selectedOption = 0;
    void Start()
    {
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= dungeonDB.DungeonCount)
        {
            selectedOption = 0;
        }


        UpdateCharacter(selectedOption);
    }


    public void UpdateCharacter(int selectedOption)
    {
        dungeonLevel = dungeonDB.GetDungeonLevel(selectedOption);
        dungeonNameText.text = (dungeonLevel.dungeonName +" "+ dungeonLevel.level.ToString()).ToUpper();
        numberBattles.text = dungeonLevel.numberOfBattles.ToString();
        dungeonDescText.text = dungeonLevel.dungeonDescription;

        int totalXP = 0;
        foreach (var item in dungeonLevel.battle)
        {
            totalXP += item.xpEarned;
        }

        xpEarned.text = totalXP.ToString();
        idBattle = dungeonLevel.level;



        if (PlayerPrefs.GetInt("dungeonLevelPassed") >= dungeonLevel.level)
        {
            dungeonCompleted.text = "PASSED";
        }
        else
        {
            dungeonCompleted.text = "NOT PASSED";
        }

    }

    public void PrevOption()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = dungeonDB.DungeonCount - 1;
        }


        UpdateCharacter(selectedOption);
    }
    public void GoBack()
    {
        SceneManager.LoadScene("3.PlayerHubScene");
    }

    public void Battle()
    {
        PlayerPrefs.SetInt("currentBattle", 1);
        PlayerPrefs.SetInt("dungeonLevel", idBattle);
        SceneManager.LoadScene("5.BattleHubScene");
    }
}
