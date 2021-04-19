using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementInput : MonoBehaviour, IMovementInput
{
    [SerializeField]
    float viewDistance = 1.5f;

    [SerializeField]
    float directionTime = 0.2f; // The amount of time between direction changes
    float nextMove = 0.0f; // When the enemy can next move

    Vector2 currentDirection = Vector2.zero;

    public float GetHorizontal()
    {
        return currentDirection.x;
    }

    public float GetVertical()
    {
        return currentDirection.y;
    }

    // Runs every frame
    void Update()
    {
        // Only allow movement once per wait time, otherwise terminate here
        if(Time.time > nextMove)
        {
            nextMove = Time.time + directionTime;
        }
        else
        {
            return;
        }

        Transform player = null;

        // Check if the player is within view distance
        foreach(Collider2D col in Physics2D.OverlapCircleAll(transform.position, viewDistance))
        {
            if(col.tag == "Player")
            {
                player = col.transform;
            }
        }

        // If the player is within view distance
        if (player != null)
        {
            currentDirection = (player.position - transform.position).normalized; // Move towards the player
        }
        // Move in a random direction
        else
        {
            int dir = Random.Range(0, 4);

            // 50% chance for either horizontal, or vertical movement
            if (dir >= 2)
            {
                // Move left/right
                currentDirection = new Vector2(dir % 2 == 1 ? 1 : -1, 0);
            }
            else
            {
                // Move up/down
                currentDirection = new Vector2(0, dir % 2 == 1 ? 1 : -1);
            }
        }
    }
}
