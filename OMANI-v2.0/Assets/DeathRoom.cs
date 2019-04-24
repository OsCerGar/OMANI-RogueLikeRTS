using System.Collections.Generic;
using UnityEngine;

public class DeathRoom : MonoBehaviour
{
    [SerializeField] int enemiesKilled, numberOfEnemies;
    [SerializeField] List<GameObject> door;
    [SerializeField] List<Animator> anim;

    [SerializeField] List<GameObject> enemySpawners;
    bool done;

    void OnEnable()
    {
        Enemy.OnDie += enemyDied;
    }


    void OnDisable()
    {
        Enemy.OnDie -= enemyDied;
    }

    private void Update()
    {
        if (done)
        {
            bool allInactive = true;
            foreach (GameObject enemySpawner in enemySpawners)
            {
                if (enemySpawner.activeSelf) { allInactive = false; }
            }
            if (allInactive)
            {
                foreach (Animator animi in anim)
                {
                    animi.SetBool("Dissapear", true);
                }
            }
        }
    }

    void enemyDied(Enemy enemy)
    {
        /*
        if (done)
        {
            enemiesKilled++;
            if (enemiesKilled >= numberOfEnemies)
            {
                foreach (Animator animi in anim)
                {
                    animi.SetBool("Dissapear", true);
                }
            }
        }
        */
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            done = true;
            if (door != null)
            {
                foreach (GameObject doorT in door)
                {

                    doorT.gameObject.SetActive(true);
                }
            }
        }
    }
}
