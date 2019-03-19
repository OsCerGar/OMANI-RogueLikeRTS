using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource combatMusic, combatStop, mainTheme;
    bool isPlaying;
    int numberOfEnemies = 0;

    void OnEnable()
    {
        Enemy.OnDie += enemyDied;
        WorldSpawnPos.OnSummon += enemySummoned;
    }


    void OnDisable()
    {
        Enemy.OnDie -= enemyDied;
        WorldSpawnPos.OnSummon -= enemySummoned;
    }

    void enemyDied(Enemy died)
    {
        numberOfEnemies--;

        StartCoroutine("stopMusic");

    }

    void enemySummoned()
    {
        if (!isPlaying)
        {
            combatMusic.Play(); mainTheme.volume = 0;
            isPlaying = true;
        }
        numberOfEnemies++;
    }

    // Start is called before the first frame update
    void Awake()
    {
        combatMusic = transform.Find("CombatMusic").GetComponent<AudioSource>();
        combatStop = transform.Find("CombatMusicStop").GetComponent<AudioSource>();
        mainTheme = transform.Find("MainTheme").GetComponent<AudioSource>();
    }

    private IEnumerator stopMusic()
    {
        if (numberOfEnemies == 0)
        {
            yield return new WaitForSeconds(4.5f);

            combatMusic.Stop();
            combatStop.Play();
            mainTheme.volume = 0.85f;
            isPlaying = false;
        }
    }

}
