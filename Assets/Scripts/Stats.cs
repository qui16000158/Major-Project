using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will allow the class to be stored using PData
[System.Serializable]
public class Stats
{
    public int level = 1; // All stats refer back to this level
    public float EXP = 0.0f;

    public float defaultEXP = 100.0f; // The required EXP for the first level upgrade
    public float expMultiplier = 1.1f;

    // These must be initialized when the entity is spawned
    public float currentHealth = 100.0f;
    public float currentStamina = 40.0f;

    public float defaultHealth = 100.0f;
    public float defaultStamina = 40.0f;

    public float multiplier = 1.1f; // Exponent for growth based on level

    public float MaxHealth
    {
        get
        {
            // This takes the default value, and increases it exponentially based on the level
            return defaultHealth * Mathf.Pow(multiplier, level);
        }
    }

    public float MaxStamina
    {
        get
        {
            return defaultStamina * Mathf.Pow(multiplier, level);
        }
    }

    // This will return the EXP required to reach the next level
    public float LevelFromEXP
    {
        get
        {
            return defaultEXP * Mathf.Pow(expMultiplier, level);
        }
    }

    public void LevelUp()
    {
        // Check if the object is able to level up
        if(EXP > LevelFromEXP)
        {
            // Increase the object's level
            level++;
            // Reset the object's EXP
            EXP = 0;

            // Increase the current stats to their new max value
            Initialize();
        }
    }

    public void Initialize()
    {
        currentHealth = MaxHealth;
        currentStamina = MaxStamina;
    }
}
