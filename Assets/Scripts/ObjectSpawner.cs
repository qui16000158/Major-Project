using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // The object to spawn
    [SerializeField]
    GameObject toSpawn;

    GameObject spawnedObject; // The spawned object within the scene

    // This will allow us to spawn enemies using events
    public void Spawn()
    {
        spawnedObject = Instantiate(toSpawn, transform.position, Quaternion.identity);
    }

    // Destroy the spawned object if it still exists.
    public void Despawn()
    {
        if (spawnedObject)
        {
            Destroy(spawnedObject);
        }
    }
}
