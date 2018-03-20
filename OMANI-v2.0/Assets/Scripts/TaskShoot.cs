using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class TaskShoot : Action {

    ShootProjectile ShootScript;
    // Use this for initialization
    void Start () {
        ShootScript = transform.gameObject.GetComponent<ShootProjectile>();

    }

    public override TaskStatus OnUpdate()
    {
        if (ShootScript != null)
        {
            ShootScript.Shoot();
        }
        return TaskStatus.Success;
    }
}
