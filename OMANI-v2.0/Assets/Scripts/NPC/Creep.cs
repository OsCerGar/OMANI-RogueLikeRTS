using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Creep : Enemy {

	public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    private void FixedUpdate()
    {
        if (AI_GetEnemy() != null)
        {
            if (AI_GetEnemy().GetComponent<NPC>()!= null)
            {
                if (AI_GetEnemy().GetComponent<NPC>().Life <= 0)
                {
                    AI_SetEnemy(null);
                }
            }
        }
    }
    public override void Die()
    {
        transform.gameObject.SetActive(false);
    }
}
