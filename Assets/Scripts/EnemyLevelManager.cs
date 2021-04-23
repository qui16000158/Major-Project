using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// QUI16000158 | James Quinney
public class EnemyLevelManager : LevelManager
{
    [SerializeField]
    TMP_Text levelDisplay;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager playerLevel = PlayerLevelManager.instance;

        // The enemy's level will be either 1 above the player's level, or 1 below. (upper bound is exclusive in Random.Range)
        stats.level = Random.Range(playerLevel.stats.level - 1, playerLevel.stats.level + 2);

        stats.Initialize(); // Initialize the enemy's stats

        // Update the enemy's level display, if they have one
        if (levelDisplay != null)
        {
            levelDisplay.text = "Level: " + stats.level;
        }
    }
}
