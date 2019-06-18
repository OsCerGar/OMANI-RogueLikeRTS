using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBallsEvents : MonoBehaviour
{
    [SerializeField]
    List<GameObject> SpawnPositions;
    int SpawnPosition;

    [SerializeField]
    float timeBetweenAttacks;
    [SerializeField]
    int points, pointsBetweenRounds;
    // Start is called before the first frame update
    void Start()
    {

        //points should change uppon difficulty
        if (GamemasterController.GameMaster.Difficulty < 1) { }
        else
        {
            points = points * GamemasterController.GameMaster.Difficulty;
        }

        IEnumerator coroutine = SpawnEnemies();
        StartCoroutine(coroutine);
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        if (timeBetweenAttacks == 0) { Debug.Log("Time between attacks cannot be 0."); }
        else
        {
            EnemyPooler.enemypool.RandomSpawnEnemies(points, SpawnPositions[SpawnPosition].transform);
            if (SpawnPosition < SpawnPositions.Count - 1) { SpawnPosition++; }
            else { SpawnPosition = 0; }
            Debug.Log("Spawned");
            points = points + pointsBetweenRounds;
            StartCoroutine("SpawnEnemies");
        }
    }
}
