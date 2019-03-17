using UnityEngine;

public class DeathRoom : MonoBehaviour
{
    [SerializeField] int enemiesKilled, numberOfEnemies;
    [SerializeField] GameObject door;
    [SerializeField] Animator anim;

    bool done;

    void OnEnable()
    {
        Enemy.OnDie += enemyDied;
    }


    void OnDisable()
    {
        Enemy.OnDie -= enemyDied;
    }

    void enemyDied(Enemy enemy)
    {
        if (done)
        {
            enemiesKilled++;
            if (enemiesKilled >= numberOfEnemies)
            {
                anim.SetBool("Dissapear", true);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            done = true;
            door.gameObject.SetActive(true);
        }
    }
}
