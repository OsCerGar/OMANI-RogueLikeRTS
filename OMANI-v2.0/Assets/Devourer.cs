using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devourer : Enemy {

    void Awake()
    {
        boyType = "Devourer";
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
