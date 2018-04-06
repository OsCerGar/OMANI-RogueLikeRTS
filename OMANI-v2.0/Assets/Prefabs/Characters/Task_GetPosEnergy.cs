using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Task_GetPosEnergy : Action
{


    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        //Creates a Sphere around himself and makes the target of every Ally the same as his
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Enemy");


        if (thisTarget.Value.GetComponentInParent<BU_PowerPlant>())
        {
            var PositiontoGo = thisTarget.Value.GetComponentInParent<BU_PowerPlant>().givePosition();
            thisTarget.Value = PositiontoGo.gameObject;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
