using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BU_Building_State : NPC
{
    [SerializeField]
    List<GameObject> buildingElements = new List<GameObject>();

    protected GameObject ruin;
    protected NavMeshObstacle obstacle;
    protected Canvas canv;

    public int scrapNeeded, ScrapCounter = 0;

    public override void Update()
    {
        if (state == "Dead")
        {
            if (ScrapCounter >= 3)
            {
                life = startLife;
                Reconstruct();
                state = "Alive";

                foreach (GameObject buildingElement in buildingElements)
                {
                    buildingElement.SetActive(true);
                }

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

    public virtual void Reconstruct()
    {
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
        foreach (GameObject buildingElement in buildingElements)
        {
            buildingElement.SetActive(false);
        }

        //GetComponent<AudioSource>().Play();
        obstacle.enabled = false;

        canv.enabled = true;
        ruin.SetActive(true);
    }

    private void Awake()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        canv = transform.Find("DeadCanvas").GetComponent<Canvas>();
        ruin = transform.Find("Ruin").gameObject;
    }

}
