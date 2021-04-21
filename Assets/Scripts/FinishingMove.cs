using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishingMove : MonoBehaviour
{
    // This will attack the opponent one final time, nullifying the effects of first move advantage
    public void DoFinishingMove()
    {
        BattleManager manager = FindObjectOfType<BattleManager>(); // Find the battle manager and cache it

        manager.EnemyAttack(); // Warrant the enemy one final attack
    }
}
