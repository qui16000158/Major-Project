using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

// QUI16000158 | James Quinney
public class BattleManager : MonoBehaviour
{
    // A list of all gameobjects paused for the battle
    public GameObject[] paused;

    public PlayerLevelManager player;
    public EnemyLevelManager enemy;

    bool isWaiting = false;
    bool battleOver = false;

    bool playerDefending;
    bool enemyDefending;

    [SerializeField]
    Button[] actionButtons; // The player's action buttons, to be disabled when the player cannot perform an action

    [SerializeField]
    TMP_Text battleText; // Announcer text that explains the most recent move

    [SerializeField]
    TMP_Text playerHealth;
    [SerializeField]
    TMP_Text enemyHealth;

    [SerializeField]
    TMP_Text playerStamina;
    [SerializeField]
    TMP_Text enemyStamina;

    [SerializeField]
    TMP_Text playerLevel;
    [SerializeField]
    TMP_Text enemyLevel;

    public enum ActionType
    {
        Attack,
        Defend,
        Wait
    }

    void ButtonVisibility(bool isVisible)
    {
        foreach(Button button in actionButtons)
        {
            button.interactable = isVisible;
        }
    }

    public void InitializeBattle()
    {
        // Ensure the player's health and stamina are full when the battle starts
        player.stats.currentHealth = player.stats.MaxHealth;
        player.stats.currentStamina = player.stats.MaxStamina;

        // Ensure the enemy's health and stamina re full when the battle starts
        enemy.stats.currentHealth = enemy.stats.MaxHealth;
        enemy.stats.currentStamina = enemy.stats.MaxStamina;

        // Display player and enemy levels
        playerLevel.text = "Level: " + player.stats.level;
        enemyLevel.text = "Level: " + enemy.stats.level;

        UpdateDisplay();
    }

    public void DoAction(ActionType action)
    {
        if (battleOver) return; // Don't allow the player to attack when the battle is over

        if (isWaiting) return; // Do not allow actions whilst waiting

        playerDefending = false;

        switch (action)
        {
            case ActionType.Attack:
                // The player can only attack if they have enough stamina
                if (player.stats.currentStamina >= 10.0f)
                {
                    PlayerAttack();
                }
                else
                {
                    battleText.text = "Player is too tired to attack!";
                }

                break;
            case ActionType.Defend:
                playerDefending = true;
                battleText.text = "Player is defending";
                break;
            case ActionType.Wait:
                player.stats.currentStamina = Mathf.Min(player.stats.currentStamina + 15.0f, player.stats.MaxStamina);
                battleText.text = "Player is waiting";

                break;
        }

        player.stats.currentStamina = Mathf.Min(player.stats.currentStamina + 5.0f, player.stats.MaxStamina);

        UpdateDisplay();

        ButtonVisibility(false); // Disable the player's buttons

        StartCoroutine(WaitForEnemyTurn());
    }

    // When the enemy gets to take their turn
    public void EnemyTurn()
    {
        if (battleOver) return; // Don't allow the enemy to continue once the battle has ended

        enemyDefending = false;

        if (player.stats.currentHealth - enemy.stats.GetDamage <= 0.0f && enemy.stats.currentStamina >= 10.0f)
        {
            EnemyAttack();
        }
        else
        {
            bool canAttack = false;
            if (enemy.stats.currentStamina >= 10.0f)
            {
                canAttack = true;
            }

            if (canAttack)
            {
                int rand = Random.Range(1, 101); // Generate a random number from 1->100

                // The enemy has an 80% chance to attack, and a 20% chance to defend
                if(rand <= 80)
                {
                    EnemyAttack();
                }
                else
                {
                    enemyDefending = true;
                    battleText.text = "Enemy is defending";
                }
            }
            else
            {
                int rand = Random.Range(0, 2);

                if (rand == 1)
                {
                    enemyDefending = true;
                    battleText.text = "Enemy is defending";
                }
                else
                {
                    enemy.stats.currentStamina = Mathf.Min(enemy.stats.currentStamina + 15.0f, enemy.stats.MaxStamina);
                    battleText.text = "Enemy is waiting";
                }
            }
        }

        enemy.stats.currentStamina = Mathf.Min(enemy.stats.currentStamina + 5.0f, enemy.stats.MaxStamina);

        UpdateDisplay();

        ButtonVisibility(true); // Re-enable the player's buttons
    }

    void PlayerAttack()
    {
        player.stats.currentStamina -= 10.0f;

        float damage = player.stats.GetDamage;
        // Damage is halved when defending
        if (enemyDefending)
        {
            damage /= 2;
        }

        enemy.stats.currentHealth -= damage;

        // Check if the enemy has died
        if ((int)enemy.stats.currentHealth <= 0)
        {
            EnemyAttack(true); // Perform the enemy's finishing move

            if((int)player.stats.currentHealth <= 0)
            {
                battleText.text = "Enemy has killed the Player with their finishing move!";
                StartCoroutine(EndBattle(player)); // End the battle and kill the player
            }
            else
            {
                battleText.text = "Player has killed the Enemy!";
                StartCoroutine(EndBattle(enemy)); // End the battle
            }
        }
        else
        {
            battleText.text = "Player dealt " + Mathf.FloorToInt(damage) + " damage to Enemy";
        }
    }

    public void EnemyAttack(bool isFinishingMove = false)
    {
        enemy.stats.currentStamina -= 10.0f;

        float damage = enemy.stats.GetDamage;
        if (playerDefending)
        {
            damage /= 2;
        }

        player.stats.currentHealth -= damage;

        // Do not check if the player is dead when performing a finishing move
        if (!isFinishingMove)
        {

            // Check if the player has died
            if ((int)player.stats.currentHealth <= 0)
            {
                battleText.text = "Enemy has killed the Player!";
                StartCoroutine(EndBattle(player)); // End the battle and kill the player
            }
            else
            {
                battleText.text = "Enemy dealt " + Mathf.FloorToInt(damage) + " damage to Player";
            }
        }
    }

    public void UpdateDisplay()
    {
        // This will update the in-scene text display, floor to int prevents floating point precision errors from appearing
        // Mathf.Max is used to ensure that health is never displayed below 0

        playerHealth.text = "Health: " + Mathf.Max((int)player.stats.currentHealth, 0);
        enemyHealth.text = "Health: " + Mathf.Max((int)enemy.stats.currentHealth, 0);

        playerStamina.text = "Stamina: " + (int)player.stats.currentStamina;
        enemyStamina.text = "Stamina: " + (int)enemy.stats.currentStamina;
    }

    // Workaround for UnityEvents
    public void DoAction(int action)
    {
        DoAction((ActionType)action);
    }

    // This will cause a delay between the player's turn, and the enemy's turn.
    IEnumerator WaitForEnemyTurn()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1);

        EnemyTurn();
        isWaiting = false;
    }

    // This will end the battle after 5 seconds, to give player's an opportunity to see how the battle ended.
    IEnumerator EndBattle(LevelManager loser)
    {
        isWaiting = true; // Stop any furthur moves from taking place
        battleOver = true;
        yield return new WaitForSeconds(5);

        OnBattleEnded(loser);
    }

    public void OnBattleEnded(LevelManager loser)
    {
        if (paused == null) return; // The game is only "paused" whilst a battle is ongoing, therefore a battle will not need to end if one is not running

        // Unpause all game objects from the old scene
        foreach(GameObject unpause in paused)
        {
            unpause.SetActive(true);
        }

        loser.onDeath.Invoke(); // Kill the loser

        paused = null; // Clean up

        AsyncOperation loading = SceneManager.UnloadSceneAsync(gameObject.scene); // Unload the battle scene

        loading.completed += (operation) =>
        {
            BattleSceneLoader.isLoaded = false;
        };
    }
}
