using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using EZObjectPools;

public class SurkaRanged : Enemy {
    [SerializeField]GameObject weapon;
    GameObject throwspear;
    private void Awake()
    {
        throwspear = GetComponentInChildren<RangedSpear>().gameObject;
    }
    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
   
    public override void AttackHit()
    {

        weapon.SetActive(false);
        throwspear.SetActive(true);
        throwspear.transform.position = transform.position;
        if (AI_GetEnemy() != null)
        {
            throwspear.GetComponent<RangedSpear>().setDestination(weapon.transform, AI_GetEnemy().transform.position + transform.forward * 2);
        }
        else
        {
            throwspear.GetComponent<RangedSpear>().setDestination(weapon.transform, this.transform.position + transform.forward * 10);
        }
    }

}
