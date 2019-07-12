using System.Collections.Generic;
using UnityEngine;

public class DeathRoom : MonoBehaviour
{
    [SerializeField] List<GameObject> door;
    [SerializeField] List<Animator> anim;

    [SerializeField] List<GameObject> enemySpawners;
    bool done;
    float timerSecureStop;
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
            if (Time.time - timerSecureStop > 20f)
            {
                foreach (Animator animi in anim)
                {
                    animi.SetBool("Dissapear", true);
                    MusicManager.musicManager.roomClosed();

                }
            }
        }
    }

    void enemyDied(Enemy enemy)
    {
        timerSecureStop = Time.time;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            timerSecureStop = Time.time;
            done = true;
            if (door != null)
            {
                foreach (GameObject doorT in door)
                {
                    doorT.gameObject.SetActive(true);
                    MusicManager.musicManager.roomStart();
                }
            }
        }
    }
}
