using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SurkaBoss: Enemy {
    [SerializeField] ParticleSystem AttackTrail;
    [SerializeField] ParticleSystem Slash;
    [SerializeField] ParticleSystem Shoot;
    [SerializeField] ParticleSystem ShootPuke;
    [SerializeField] string TreeToEnable;

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
        Attackzone.SetActive(true);
        Slash.Play();
    }
    public void Spit()
    {
        Shoot.Emit(2);
        SM.AttackHit();
    }
    public void Puke()
    {
        ShootPuke.Play();
    }

    public override void Start()
    {
        base.Start();
        enableTree(TreeToEnable);

    }
}
