using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleHubManager : MonoBehaviour
{
    public DungeonDB dungeonDB;

    public Text dungeonLevelText;
    public Text xpEarned;
    public Text currentBattle;
    public Text description;
    public Text beastClass;
    public SpriteRenderer enemySprite;

    private DungeonLevel dungeonLevel;

    void Start()
    {
        UpdateCharacter(PlayerPrefs.GetInt("dungeonLevel"));
    }


    public void UpdateCharacter(int selectedOption)
    {
        dungeonLevel = dungeonDB.GetDungeonLevelByLevel(selectedOption);
        dungeonLevelText.text = (dungeonLevel.dungeonName+ " " + dungeonLevel.level.ToString()).ToUpper();
        currentBattle.text = "BATTLE " + PlayerPrefs.GetInt("currentBattle") + "/" + dungeonLevel.numberOfBattles.ToString();


        for (int i = 0; i < dungeonLevel.numberOfBattles; i++)
        {
            if (PlayerPrefs.GetInt("currentBattle") == i + 1)
            {
                xpEarned.text = dungeonLevel.battle[i].xpEarned.ToString();
                enemySprite.sprite = dungeonLevel.battle[i].portraitSprite;
                description.text = dungeonLevel.battle[i].Description;
                beastClass.text = dungeonLevel.battle[i].className.ToUpper();
            }
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("3.PlayerHubScene");
    }

    public void Battle()
    {
        //   PlayerPrefs.SetString("dungeonLevel", dungeonLevelText.text);
        SceneManager.LoadScene("6.BattleScene");
    }
}
