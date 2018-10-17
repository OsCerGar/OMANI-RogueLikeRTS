using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int DangerPoints;
    private int MinimumDangerCost = 15, DangerCostMedium = 25;
    List<Transform> EnemySpawningPos = new List<Transform>();
    EnemyPooler EPooler;
    List<String> Enemies = new List<string>();
    List<GameObject> ActiveEnemies = new List<GameObject>();
    int i;

    // Use this for initialization
    void Start()
    {
        DangerPoints = (int)Vector3.Distance(FindObjectOfType<BU_Energy>().transform.position, transform.position);
        EPooler = FindObjectOfType<EnemyPooler>();
        var EnemyPos = GetComponentsInChildren<WorldSpawnPos>();
        foreach (var item in EnemyPos)
        {
            EnemySpawningPos.Add(item.transform);
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            while (DangerPoints > MinimumDangerCost)
            {
                CreateDanger();
            }
            foreach (var enemy in Enemies)
            {
                var SpawnedEnemy = EPooler.SpawnEnemy(enemy, EnemySpawningPos[UnityEngine.Random.Range(0, EnemySpawningPos.Count)]);
                ActiveEnemies.Add(SpawnedEnemy);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            foreach (var enemy in ActiveEnemies)
            {
                if (enemy != null)
                {
                    enemy.SetActive(false);
                }
            }
            ActiveEnemies.Clear();
        }

    }


    private void CreateDanger()
    {
        if (EnemySpawningPos.Count > 0)
        {
            DangerPoints -= MinimumDangerCost;
            i = UnityEngine.Random.Range(0, 1);
            switch (i)
            {
                case 0:
                    Enemies.Add("SurkaMele");
                    DangerPoints -= DangerCostMedium;

                    break;

                case 1:
                    Enemies.Add("SurkaRat");
                    DangerPoints -= MinimumDangerCost;
                    break;

            }
        }

    }
}
