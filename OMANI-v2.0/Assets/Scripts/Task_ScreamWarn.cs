using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Task_ScreamWarn : Action
{
	

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        //Creates a Sphere around himself and makes the target of every Ally the same as his
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Enemy");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
        
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == transform.tag)
            {

                var targetVariable = (SharedGameObject)hitColliders[i].gameObject.GetComponent<BehaviorTree>().GetVariable("Target");
                targetVariable.Value = thisTarget.Value;
            }

            i++;
        }
        return TaskStatus.Success;
    }
}
