using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class TaskShoot : Action {

    ShootProjectile ShootScript;
    // Use this for initialization
    void Start () {

    }

    public override TaskStatus OnUpdate()
    {
        ShootScript = transform.gameObject.GetComponent<ShootProjectile>();
        if (ShootScript != null)
        {
            Debug.Log("Turret_Shoot");
            ShootScript.Shoot();
        }
        return TaskStatus.Success;
    }
}
