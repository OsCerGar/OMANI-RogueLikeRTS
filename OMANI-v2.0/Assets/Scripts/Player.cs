using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : NPC
{
    /*
    public delegate void OnDeath();
    public static event OnDeath playerDead;
    */

    void Awake()
    {
        boyType = "Swordsman";

        quarter = Mathf.RoundToInt(startLife * 0.25f);
        half = Mathf.RoundToInt(startLife * 0.5f);
        quarterAndHalf = Mathf.RoundToInt(startLife * 0.75f);
    }

    public override void Die()
    {
        SceneManager.LoadScene("GAME_ALPHA", LoadSceneMode.Single);

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
                sumAmount += increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, quarter);
                    sumAmount = 0;
                }
            }
            else if (life < half)
            {
                sumAmount += increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, half);
                    sumAmount = 0;

                }
            }
            else if (life < quarterAndHalf)
            {
                sumAmount += increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, quarterAndHalf);
                    sumAmount = 0;

                }
            }
            else if (life < startLife)
            {
                sumAmount += increaseAmount * Time.unscaledDeltaTime;
                if (sumAmount > 1)
                {
                    life = Mathf.Clamp(life + 1, 0, startLife);
                    sumAmount = 0;

                }
            }

        }
    }



    #endregion


}

