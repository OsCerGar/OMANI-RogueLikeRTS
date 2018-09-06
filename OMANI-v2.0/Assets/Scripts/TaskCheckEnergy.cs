using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class TaskCheckEnergy : Action {
    Robot_Energy rEnergy;
    float speed = 0.1f;
    // Use this for initialization

    public override TaskStatus OnUpdate()
    {

        if (rEnergy == null)
        {
            rEnergy = transform.gameObject.GetComponent<Robot_Energy>();
        }
        if (rEnergy.ready == true)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
