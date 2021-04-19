using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPGiver : MonoBehaviour
{
    // This will give EXP to the player
    public void GiveEXP(int amount)
    {
        PlayerLevelManager.instance.stats.AddEXP(amount);
    }
}
