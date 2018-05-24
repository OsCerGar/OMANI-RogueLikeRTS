using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wall : BU_Building_State
{
    Collider collider;
    Renderer renderer;

    public override void Update()
    {
        if (state == "Dead")
        {
            if (ScrapCounter >= 3)
            {
                life = startLife;
                Reconstruct();
                state = "Alive";

            }
        }
        if (life <= 0)
        {
            if (state != "Dead")
            {
                //provisional :D
                state = "Dead";
                Die();
            }
        }
    }

    public override void Reconstruct()
    {
        collider.enabled = true;
        renderer.enabled = true;
        obstacle.enabled = true;

        canv.enabled = false;

        ruin.SetActive(false);
    }

    public override void Heal(int _heal)
    {
        if (state == "Dead")
        {
            ScrapCounter++;
        }
    }
    public override void Die()
    {
        GetComponent<AudioSource>().Play();
        collider.enabled = false;
        renderer.enabled = false;
        obstacle.enabled = false;

        canv.enabled = true;

        ruin.SetActive(true);
    }

    private void Awake()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
        obstacle = GetComponent<NavMeshObstacle>();
        canv = transform.Find("Canvas").GetComponent<Canvas>();
        ruin = transform.Find("Ruin").gameObject;
    }

}
