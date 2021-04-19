using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : LevelManager
{
    public static LevelManager instance;
    public static Stats playerStats = null;

    // Awake is called before Start where enemies are initialized
    void Awake()
    {
        // This will ensure that the player's stats persists between scenes
        if(playerStats != null)
        {
            stats = playerStats;
        }
        else
        {
            playerStats = stats;
        }
        instance = this; // This will make the player's level manager easily accessible for enemies
    }
}
