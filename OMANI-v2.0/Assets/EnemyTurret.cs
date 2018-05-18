using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{

    void Awake()
    {
        boyType = "Turret";
    }
    public override void Update()
    {
        if (state == "Alive")
        {
            if (life <= 0)
            {
                //provisional :D
                Die();
                state = "Dead";
            }
        }
    }
    public override void Die()
    {
        Destroy(transform.gameObject);
    }


}