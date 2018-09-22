using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SurkaMelee : Enemy {
    [SerializeField] ParticleSystem AttackTrail;
    [SerializeField] private UI_SurkaMAttack uI_secondAttack;
    [SerializeField] GameObject SecondAttackZone;
   
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
    public void StartFillSecondAttackUI(float _time)
    {

        uI_secondAttack.startFill(_time);
    }
    public void SecondAttackHit()
    {
        SecondAttackZone.SetActive(true);
    }

}
