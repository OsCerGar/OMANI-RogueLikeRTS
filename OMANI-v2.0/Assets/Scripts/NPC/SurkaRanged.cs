using BehaviorDesigner.Runtime;
using UnityEngine;

public class SurkaRanged : Enemy
{
    [SerializeField] ParticleSystem AttackTrail;
    [SerializeField] ParticleSystem Shoot;


    private void Awake()
    {
        base.Awake();

    }

    public void Start()
    {
        base.Start();

        //damage
        damage = int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaRanged")[2]);
        damage = Mathf.RoundToInt(damage + (GamemasterController.GameMaster.Difficulty * 2));
        //life
        life = int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaRanged")[1]);
    }
    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    public void StartAttackTrail()
    {
        AttackTrail.Play();
    }
    public override void Die()
    {
        GamemasterController.GameMaster.Money += int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaRanged")[3]);
        base.Die();
    }
    public override void AttackHit()
    {
        Shoot.Emit(1);
        SM.AttackHit();
    }

}
