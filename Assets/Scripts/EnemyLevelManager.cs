using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelManager : LevelManager
{
    // Start is called before the first frame update
    void Start()
    {
        LevelManager playerLevel = PlayerLevelManager.instance;

        // The enemy's level will be either 1 above the player's level, or 1 below. (upper bound is exclusive in Random.Range)
        stats.level = Random.Range(playerLevel.stats.level - 1, playerLevel.stats.level + 2);

        stats.Initialize(); // Initialize the enemy's stats
    }
}
