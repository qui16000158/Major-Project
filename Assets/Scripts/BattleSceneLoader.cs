using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneLoader : MonoBehaviour
{
    public static bool isLoading = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isLoading)
        {
            isLoading = true;
            AsyncOperation loaded = SceneManager.LoadSceneAsync("Battle", LoadSceneMode.Additive);

            loaded.completed += (operation) => {

                GameObject[] toPause = FindObjectsOfType<GameObject>();

                // Make all game objects from the previous scene inactive
                foreach(GameObject pause in toPause)
                {
                    if (pause.scene == gameObject.scene)
                    {
                        pause.SetActive(false);
                    }
                }

                BattleManager manager = GameObject.FindGameObjectWithTag("Battle Manager").GetComponent<BattleManager>();

                manager.player = collision.GetComponent<PlayerLevelManager>();
                manager.enemy = GetComponent<EnemyLevelManager>();

                manager.paused = toPause;

                manager.InitializeBattle();
            };
        }
    }
}
