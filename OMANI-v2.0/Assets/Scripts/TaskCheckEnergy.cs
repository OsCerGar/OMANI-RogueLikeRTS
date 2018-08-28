using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class TaskCheckEnergy : Action {
    Robot npc;
    public SharedVector3 RightVector;
    float speed = 0.1f;
    // Use this for initialization

    public override TaskStatus OnUpdate()
    {

        if (npc == null)
        {
            npc = transform.gameObject.GetComponent<Robot>();
        }
        if (npc.currentenergy >= npc.energycap)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
