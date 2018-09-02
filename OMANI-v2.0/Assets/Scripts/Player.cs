using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NPC
{
    /*
    public delegate void OnDeath();
    public static event OnDeath playerDead;
    */
    float increaseAmount = 2, sumAmount, lastLife;
    int quarter, half, quarterAndHalf;

    void Awake()
    {
        boyType = "Swordsman";

        quarter = Mathf.RoundToInt(startLife * 0.25f);
        half = Mathf.RoundToInt(startLife * 0.5f);
        quarterAndHalf = Mathf.RoundToInt(startLife * 0.75f);
    }

    public override void Die()
    {
        // playerDead();
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

            #region IncreaseLifePool

            if (life < quarter)
            {
                sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, quarter);
                }
            }
            else if (life < half)
            {
                sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, half);
                }
            }
            else if (life < quarterAndHalf)
            {
                sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, quarterAndHalf);
                }
            }
            else if (life < startLife)
            {
                sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, startLife);
                }
            }

            sumAmount = 0;
        }
    }



    #endregion


}

