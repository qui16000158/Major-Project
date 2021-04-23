using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// QUI16000158 | James Quinney
public class AreaManager : MonoBehaviour
{
    // Events for when the player enters/exits the area
    [SerializeField]
    UnityEvent onPlayerEntered;
    [SerializeField]
    UnityEvent onPlayerExit;
    Transform spawnPoint; // Where the player spawns in the area

    void Awake()
    {
        spawnPoint = transform.Find("Spawn Point");
    }

    // Invokes an event when the player enters the area
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            onPlayerEntered.Invoke();

            if (spawnPoint)
            {
                other.transform.position = spawnPoint.position; // Move the player to the spawn point
            }
        }
    }

    // Invokes an event when the player exits the area
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" && !BattleSceneLoader.isLoaded)
        {
            onPlayerExit.Invoke();
        }
    }
}
