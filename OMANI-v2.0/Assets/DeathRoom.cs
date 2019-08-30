using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathRoom : MonoBehaviour
{
    [SerializeField] List<GameObject> door;
    [SerializeField] List<Animator> anim;

    [SerializeField] List<GameObject> enemySpawners;
    [SerializeField] int timeToStop;

    bool entered;
    float timer;
    int surkaMeleeKilled, corruptedDemonKilled, surkaRangedKilled;

    [SerializeField]
    int enemiesToKillFor3Stars;
    int stars = 0;
    int pointsToAdd = 0;

    #region UI STUFF
    [SerializeField] Text timeText;
    [SerializeField] public Image starImage;
    [SerializeField] public Sprite Stars0, Stars1, Stars2, Stars3;
    #endregion


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
        if (entered)
        {        //Stop deathRoom.

            timer += Time.deltaTime;
            if (timer >= timeToStop)
            {
                foreach (GameObject enemySpawner in enemySpawners)
                {
                    enemySpawner.SetActive(false);
                }
                foreach (Animator animi in anim)
                {
                    animi.SetBool("Dissapear", true);
                }
                entered = false;
                MusicManager.musicManager.roomClosed();
                Enemy.OnDie -= enemyDied;
                timeText.enabled = false;
                starImage.enabled = false;
                AddPoints();
                enabled = false;
            }

            //UI STUFF
            timeText.text = Mathf.RoundToInt(timeToStop - timer).ToString();

        }
    }

    private void enemyDied(Enemy enemy)
    {
        if (entered)
        {
            switch (enemy.boyType)
            {
                case "SurkaMelee":
                    surkaMeleeKilled++;
                    break;
                case "SurkaRanged":
                    surkaRangedKilled++;
                    break;
                case "CorruptedDemon":
                    corruptedDemonKilled++;
                    break;
            }
            int enemiesKilled = 0;
            enemiesKilled = surkaMeleeKilled + surkaRangedKilled + corruptedDemonKilled;
            if (enemiesKilled >= Mathf.RoundToInt(enemiesToKillFor3Stars / 3))
            {
                stars = 1;
                starImage.sprite = Stars1;
            }
            if (enemiesKilled >= Mathf.RoundToInt(enemiesToKillFor3Stars / 2))
            {
                stars = 2;
                starImage.sprite = Stars2;
            }
            if (enemiesKilled >= enemiesToKillFor3Stars)
            {
                stars = 3;
                starImage.sprite = Stars3;
            }

            PointsCalc();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !entered)
        {
            entered = true;
            if (door != null)
            {
                foreach (GameObject doorT in door)
                {
                    doorT.gameObject.SetActive(true);
                    MusicManager.musicManager.roomStart();
                }
            }
            timeText.enabled = true;
            starImage.enabled = true;
        }
    }

    private void PointsCalc()
    {
        pointsToAdd = 0;
        pointsToAdd += surkaMeleeKilled * int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaMelee")[3]);
        pointsToAdd += surkaRangedKilled * int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaRanged")[3]);
        pointsToAdd += corruptedDemonKilled * int.Parse(GamemasterController.GameMaster.getCsvValues("CorruptedDemon")[3]);
    }

    private void AddPoints()
    {
        int pointsAdded = 0;
        if (stars == 1)
        { //add 10% of the pointsToAdd
            pointsAdded = Mathf.RoundToInt(pointsToAdd * 0.1f);
        }
        if (stars == 2)
        { //add 20% of the pointsToAdd
            pointsAdded = Mathf.RoundToInt(pointsToAdd * 0.2f);

        }
        if (stars == 3)
        { //add 30% of the pointsToAdd
            pointsAdded = Mathf.RoundToInt(pointsToAdd * 0.3f);

        }
        //add points
        Debug.Log("points added " + pointsAdded);
    }
}
