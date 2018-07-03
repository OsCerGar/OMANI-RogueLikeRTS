using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NPC
{

    public delegate void OnDeath();
    public static event OnDeath playerDead;

    void Awake()
    {
        boyType = "Swordsman";
    }

    public override void Die()
    {
        playerDead();
    }

    public override void Update()
    {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral
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
}
