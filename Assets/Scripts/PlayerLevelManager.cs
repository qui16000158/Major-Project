using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : LevelManager
{
    public static LevelManager instance;

    // Awake is called before Start where enemies are initialized
    void Awake()
    {
        instance = this; // This will make the player's level manager easily accessible for enemies
    }
}
