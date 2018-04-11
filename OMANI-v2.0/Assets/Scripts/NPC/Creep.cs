using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Creep : NPC {

	public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    private void FixedUpdate()
    {
        if (AI_GetTarget() != null)
        {
            if (AI_GetTarget().GetComponent<NPC>()!= null)
            {
                if (AI_GetTarget().GetComponent<NPC>().Life <= 0)
                {
                    AI_SetTarget(null);
                }
            }
        }
    }
}
