using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// QUI16000158 | James Quinney
public class LevelUpHandler : MonoBehaviour
{
    [SerializeField]
    UnityEvent succeeded;
    [SerializeField]
    UnityEvent failed;
    [SerializeField]
    LevelManager levelManager;
    
    // Attempt to level up the player, then call the necessary events
    public void TryLevelUp()
    {
        // Store the current level before levelling up, this will allow us to check if the level has changed
        int currentLevel = levelManager.stats.level;

        levelManager.LevelUp(); // Attempt to level up

        // Check if the level has changed, and invoke the necessary events
        if(currentLevel < levelManager.stats.level)
        {
            succeeded.Invoke();
        }
        else
        {
            failed.Invoke();
        }
    }
}
