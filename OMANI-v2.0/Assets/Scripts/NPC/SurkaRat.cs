﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SurkaRat : Enemy {
    [SerializeField] ParticleSystem AttackTrail;
   
    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    public void ShowAttackUI()
    {
          UI_Attack.Show(transform.gameObject);
    }
    public void StartAttackTrail()
        {
             AttackTrail.Play();
        }


}
