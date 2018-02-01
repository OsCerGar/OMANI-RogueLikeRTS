using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class TaskOrbitate : Action {

    public SharedVector3 RightVector;
    float speed = 0.1f;
    // Use this for initialization
    void Start () {

	}

    public override TaskStatus OnUpdate()
    {

        RightVector.SetValue(transform.right*speed);
        return TaskStatus.Success;
    }
}
