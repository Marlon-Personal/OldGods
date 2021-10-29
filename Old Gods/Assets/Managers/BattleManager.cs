using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleManager : MonoBehaviour
{
    public DungeonDB dungeonDB;
    public LevelDB levelDB;

    public Animator enemyAnimator;

    public Animator playerAnimator;

    //Battle
    public Text beastClass;
    public Text dungeonName;
    public Text battleNumber;
    //public Text race;
    //  public Text dificulty;
    private int XP;
    public Slider enemyHPSlider;


    private DungeonLevel dungeonLevel;

    Character player;

    //Player
    public Text classText;
    public Text lvlText;
    private int xpText;
    private int AttackText;
    private int DefenseText;
    private int HPText;
    private int SpeedText;
    public Slider playerHPSlider;

    //Attacks
    public Text attackName;
    private int attackDamage;
    public Text attackName2;
    private int attackDamage2;

    //Battle 
    public Text dialogue;
    public Text hpEnemyText;
    public Text hpPlayerText;
    private int playerRemainingHP;
    private int enemyRemainingHP;
    private int enemyTempDefense;
    private int playerTempDefense;
    private Beast beast;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(LoadBattle());
    }

    IEnumerator LoadBattle()
    {

        //   PlayerPrefs.GetInt("currentBattle"); will retrieve the battle we are in
        LoadPlayer();
        dungeonLevel = dungeonDB.GetDungeonLevelByLevel(PlayerPrefs.GetInt("dungeonLevel"));//We retrieve the dungeon with all its battles 

        for (int i = 0; i < dungeonLevel.numberOfBattles; i++)
        {
            if (PlayerPrefs.GetInt("currentBattle") == i + 1)
            {
                beast = new Beast();
                beast.className = dungeonLevel.battle[i].className;
                //Selects the correct battle, we fill the object to use it in the rest of the battle 
                beast.xpEarned = dungeonLevel.battle[i].xpEarned;
                beast.attack1 = dungeonLevel.battle[i].attack1;
                beast.attack2 = dungeonLevel.battle[i].attack2;
                beast.attack = dungeonLevel.battle[i].attack;
                beast.defense = dungeonLevel.battle[i].defense;
                beast.speed = dungeonLevel.battle[i].speed;
                beast.hp = dungeonLevel.battle[i].hp;
                break;
            }
        }

        //We print the data on the screen
        //     enemySprite.sprite = battle.oponentSprite;
        XP = beast.xpEarned;
        enemyTempDefense = beast.defense;
        enemyRemainingHP = beast.hp;
        enemyHPSlider.maxValue = beast.hp;
        enemyHPSlider.value = enemyRemainingHP;
        hpEnemyText.text = beast.hp + "/" + beast.hp;
        enemyAnimator.SetInteger("EnemySprite", PlayerPrefs.GetInt("currentBattle"));

        beastClass.text = beast.className;
        beastClass.text = dungeonLevel.dungeonName +" "+dungeonLevel.level;
        battleNumber.text = "BATTLE " + PlayerPrefs.GetInt("currentBattle") + "/" + dungeonLevel.numberOfBattles;

        dialogue.text = "The battle against " + beast.className + " is about to start!";
        yield return new WaitForSeconds(2f);


        if (SpeedText >= beast.speed)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }

    }

    void LoadPlayer()
    {
        player = new Character();
        player.LoadPlayer();

        xpText = PlayerPrefs.GetInt("xp");
        lvlText.text = "Level " + PlayerPrefs.GetInt("lvl").ToString();
        AttackText = player.AttackStat;
        DefenseText = player.DefenseStat;
        SpeedText = player.SpeedStat;
        HPText = player.hpStat;
        classText.text = player.characterClass;
        attackName.text = player.attack1.attackName.ToUpper();
        attackDamage = player.attack1.attackDamage;
        attackName2.text = player.attack2.attackName.ToUpper();
        attackDamage2 = player.attack2.attackDamage;
        playerTempDefense = player.DefenseStat;

        playerAnimator.SetInteger("PlayerSprite", player.spriteID);

        playerRemainingHP = player.hpStat;
        playerHPSlider.maxValue = player.hpStat;
        playerHPSlider.value = playerRemainingHP;
        hpPlayerText.text = player.hpStat + "/" + player.hpStat;
    }

    void PlayerTurn()
    {
        enemyAnimator.SetBool("EnemyDamage", false);
        enemyAnimator.SetBool("EnemyDeath", false);
        dialogue.text = "Choose an action";
        //    yield return new WaitForSeconds(2f);
        playerTempDefense = DefenseText;
    }

    void EnemyTurn()
    {
        enemyAnimator.SetBool("EnemyDamage", false);
        enemyAnimator.SetBool("EnemyDeath", false);
        dialogue.text = "Enemy's turn";
        //   yield return new WaitForSeconds(2f);
        enemyTempDefense = beast.defense;
        RamdonSelection();
    }


    public void QuitGame()
    {
        SceneManager.LoadScene("3.PlayerHubScene");
    }

    IEnumerator PlayerAttack(int attackNumber)
    {
        bool isDead = false;
        if (attackNumber == 1)
        {
            dialogue.text = "You have used the attack " + attackName.text;
            playerAnimator.SetBool("PlayerAttack", true);
            yield return new WaitForSeconds(1f);

            playerAnimator.SetBool("PlayerAttack", false);
            isDead = EnemyTakeDamage(1);


        }
        else if (attackNumber == 2)
        {
            dialogue.text = "You have used the special attack " + attackName2.text;
            playerAnimator.SetBool("PlayerAttack", true);
            yield return new WaitForSeconds(1f);

            playerAnimator.SetBool("PlayerAttack", false);
            isDead = EnemyTakeDamage(2);
        }
        yield return new WaitForSeconds(0.3f);
        enemyAnimator.SetBool("EnemyDamage", false);
        if (isDead)
        {
            enemyAnimator.SetBool("EnemyDead", true);
            StartCoroutine(PlayerWins());
        }
        else
        {
            yield return new WaitForSeconds(1f);
            EnemyTurn();
        }
    }



    bool EnemyTakeDamage(int attackNumber)
    {
        int attackDamageInTurn = 0;
        if (attackNumber == 1)
        {
            attackDamageInTurn = (attackDamage + (AttackText / 2)) - beast.defense;
        }
        else if (attackNumber == 2)
        {
            attackDamageInTurn = (attackDamage2 + (AttackText / 2)) - beast.defense;
        }

        if (attackDamageInTurn > 0)
        {
            enemyAnimator.SetBool("EnemyDamage", true);
            enemyRemainingHP -= attackDamageInTurn;

            dialogue.text = "The enemy has taken " + attackDamageInTurn + " damage points.";
            enemyHPSlider.value = enemyRemainingHP;

            hpEnemyText.text = enemyRemainingHP + "/" + beast.hp;
        }




        state = BattleState.ENEMYTURN;
        if (enemyRemainingHP <= 0)
        {

            return true;
        }
        else
        {

            return false;
        }

    }

    //Buttons calls 
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack(1));
    }

    public void OnAttackButton2()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack(2));
    }


    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerBlock());

    }

    IEnumerator PlayerBlock()
    {
        playerTempDefense = playerTempDefense + playerTempDefense / 2;
        yield return new WaitForSeconds(1f);
        dialogue.text = "Your defense has a temporal boost of " + playerTempDefense / 2;
        if (isPlayerInjured())
        {
            int healAmount = (HPText - playerRemainingHP) / 2;
            playerRemainingHP += healAmount;
            playerHPSlider.value = playerRemainingHP;
            yield return new WaitForSeconds(1f);
            dialogue.text = "You have healed " + healAmount + " points.";
        }


        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(1f);
        EnemyTurn();
    }

    public bool isPlayerInjured()
    {
        if (playerRemainingHP >= HPText)
        {
            return false;
        }
        else
        {
            return true;
        }
    }



    //When the enemy is dead logic
    IEnumerator PlayerWins()
    {
        //XP earned (and level up logic) 
        dialogue.text = "You have defeated the oponent!";
        int totalXP = beast.xpEarned + PlayerPrefs.GetInt("xp");
        PlayerPrefs.SetInt("xp", totalXP);
        yield return new WaitForSeconds(1f);
        dialogue.text = "You have won " + beast.xpEarned + " experience points!";
        yield return new WaitForSeconds(1f);

        isPlayerLeveledUp();
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("battlesWon", PlayerPrefs.GetInt("battlesWon") + 1);
        PlayerPrefs.SetInt("currentBattle", PlayerPrefs.GetInt("currentBattle") + 1);
        if (PlayerPrefs.GetInt("currentBattle") > dungeonLevel.numberOfBattles)
        {
            SceneManager.LoadScene("3.PlayerHubScene");
        }
        else
        {
            SceneManager.LoadScene("5.BattleHubScene");
        }
        //Show message 
        //End battle, move to main screen 
    }

    public void isPlayerLeveledUp()
    {
        int currentLevel = PlayerPrefs.GetInt("lvl");
        Level nextLevel = levelDB.GetLevelByLevel(currentLevel + 1);
        int xpNeededToLvlUp = nextLevel.xpNeeded;

        if (PlayerPrefs.GetInt("xp") >= xpNeededToLvlUp)
        {
            //Player has leveled up
            dialogue.text = "You have leveled up!";
            PlayerPrefs.SetInt("lvl", currentLevel + 1);
            PlayerPrefs.SetInt("attackPoints", PlayerPrefs.GetInt("attackPoints") + 3);
            StatsBoost();
        }

    }

    public void StatsBoost()
    {
        Random rnd = new Random();
        player.hpStat = player.hpStat + rnd.Next(1, 3);
        player.AttackStat = player.AttackStat + rnd.Next(1, 3);
        player.DefenseStat = player.DefenseStat + rnd.Next(1, 3);
        player.SpeedStat = player.SpeedStat + rnd.Next(1, 3);
        SaveSystem.SavePlayer(player);
    }

    //Enemy's Logic 
    public void RamdonSelection()
    {
        Random rnd = new Random();
        int selection = rnd.Next(1, 3);

        switch (selection)
        {
            case 1:
                StartCoroutine(EnemyAttack(1));
                break;
            case 2:
                StartCoroutine(EnemyAttack(2));
                break;
            case 3:
                StartCoroutine(EnemyBlock());
                break;

        }


    }

    IEnumerator EnemyAttack(int attackNumber)
    {
        bool isDead = false;
        yield return new WaitForSeconds(1f);
        if (attackNumber == 1)
        {
            dialogue.text = "Your opponent has used the attack " + beast.attack1;
            enemyAnimator.SetBool("EnemyAttacking", true);
            yield return new WaitForSeconds(1f);

            enemyAnimator.SetBool("EnemyAttacking", false);
            isDead = PlayerTakeDamage(1);
        }
        else if (attackNumber == 2)
        {
            dialogue.text = "Your opponent has used the special attack " + beast.attack2;
            enemyAnimator.SetBool("EnemyAttacking", true);
            yield return new WaitForSeconds(1f);

            enemyAnimator.SetBool("EnemyAttacking", false);
            isDead = PlayerTakeDamage(2);
        }
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("PlayerDamage", false);
        if (isDead)
        {
            dialogue.text = "You have lost the battle";
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("3.PlayerHubScene");
        }
        else
        {
            yield return new WaitForSeconds(1f);
            PlayerTurn();
        }
    }

    bool PlayerTakeDamage(int attackNumber)
    {
        int attackDamageInTurn = 0;
        if (attackNumber == 1)
        {
            attackDamageInTurn = (beast.attack1.attackDamage + (beast.attack / 2)) - playerTempDefense;
        }
        else if (attackNumber == 2)
        {
            attackDamageInTurn = (beast.attack2.attackDamage + (beast.attack / 2)) - playerTempDefense;
        }

        if (attackDamageInTurn <= 0)
        {
            dialogue.text = "You have not taken any damage.";
        }
        else
        {
            playerAnimator.SetBool("PlayerDamage", true);
            playerRemainingHP -= attackDamageInTurn;
            dialogue.text = "You have taken " + attackDamageInTurn + " damage points.";
            playerHPSlider.value = playerRemainingHP;
            hpPlayerText.text = playerRemainingHP + "/" + player.hpStat;
        }


        state = BattleState.PLAYERTURN;
        if (playerRemainingHP <= 0)
        {
            return true;
        }
        else
            return false;



    }
    IEnumerator EnemyBlock()
    {
        enemyTempDefense = enemyTempDefense + enemyTempDefense / 2;
        yield return new WaitForSeconds(1f);
        dialogue.text = "Your oponent's defense has a temporal boost of " + playerTempDefense / 2;
        if (isEnemyInjured())
        {
            int healAmount = (beast.hp - enemyRemainingHP) / 2;
            enemyRemainingHP += healAmount;
            yield return new WaitForSeconds(1f);
            dialogue.text = "Your oponent healed " + healAmount + " points.";
        }

        state = BattleState.PLAYERTURN;
        yield return new WaitForSeconds(1f);
        PlayerTurn();
    }

    public bool isEnemyInjured()
    {
        if (enemyRemainingHP >= beast.hp)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
