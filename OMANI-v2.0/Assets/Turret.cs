using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;



public class Turret : NPC
{

    public BehaviorTree Cannon;
    public bool energy = false;

    void Awake()
    {
        boyType = "Turret";
    }
    public override void Update()
    {
        if (!energy)
        {
            Cannon.enabled = false;
        }

        else
        {
            Cannon.enabled = true;
        }

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
    void setEnemyForCannon(GameObject _Enem)
    {
        var targetVariable = (SharedGameObject)Cannon.GetVariable("Enemy");
        targetVariable.Value = _Enem;
    }
    public override void Die()
    {
        Destroy(transform.gameObject);
    }


}