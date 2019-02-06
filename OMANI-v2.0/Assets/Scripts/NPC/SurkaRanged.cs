using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SurkaRanged : Enemy {
    [SerializeField] ParticleSystem AttackTrail;
    [SerializeField] ParticleSystem Shoot;

    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    public void StartAttackTrail()
        {
             AttackTrail.Play();
        }

    public override void AttackHit()
    {
        Shoot.Emit(1);
    }

}
