using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{
    // A list of all gameobjects paused for the battle
    public GameObject[] paused;

    public PlayerLevelManager player;
    public EnemyLevelManager enemy;

    bool isWaiting = false;

    bool playerDefending;
    bool enemyDefending;

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

        StartCoroutine(WaitForEnemyTurn());
    }

    // When the enemy gets to take their turn
    public void EnemyTurn()
    {
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
                int rand = Random.Range(0, 2);

                if(rand == 1)
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
        if (enemy.stats.currentHealth <= 0.0f)
        {
            OnBattleEnded();
            enemy.onDeath.Invoke();
        }

        battleText.text = "Player dealt " + damage + " damage to Enemy";
    }

    void EnemyAttack()
    {
        enemy.stats.currentStamina -= 10.0f;

        float damage = enemy.stats.GetDamage;
        if (playerDefending)
        {
            damage /= 2;
        }

        player.stats.currentHealth -= damage;

        // Check if the player has died
        if (player.stats.currentHealth <= 0.0f)
        {
            OnBattleEnded();
            player.onDeath.Invoke(); // Run the player's death event
        }

        battleText.text = "Enemy dealt " + damage + " damage to Player";
    }

    public void UpdateDisplay()
    {
        playerHealth.text = "Health: " + player.stats.currentHealth;
        enemyHealth.text = "Health: " + enemy.stats.currentHealth;

        playerStamina.text = "Stamina: " + player.stats.currentStamina;
        enemyStamina.text = "Stamina: " + enemy.stats.currentStamina;
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

    public void OnBattleEnded()
    {
        // Unpause all game objects from the old scene
        foreach(GameObject unpause in paused)
        {
            unpause.SetActive(true);
        }

        AsyncOperation loading = SceneManager.UnloadSceneAsync(gameObject.scene); // Unload the battle scene

        loading.completed += (operation) =>
        {
            BattleSceneLoader.isLoaded = false;
        };
    }
}
