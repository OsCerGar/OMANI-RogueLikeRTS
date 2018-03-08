using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class TaskPickUp : Action
{


    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        //Creates a Sphere around himself and makes the target of every Ally the same as his
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Target");
        Debug.Log("TODO : Add Home Var");
        var ob = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Enemy");
        transform.GetComponent<ShepHerd>().Obj = (GameObject)ob.Value;


        var thisHome = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Altar");
        thisTarget.Value = thisHome.Value;



        return TaskStatus.Success;
    }
}
