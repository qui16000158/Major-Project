﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public Stats stats;
    public UnityEvent onDeath;

    // A method that can be used to level up
    public void LevelUp()
    {
        stats.LevelUp();
    }


}
