using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Stats stats;

    // A method that can be used to level up
    public void LevelUp()
    {
        stats.LevelUp();
    }
}
