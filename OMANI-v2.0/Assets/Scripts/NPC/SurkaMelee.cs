using BehaviorDesigner.Runtime;
using UnityEngine;

public class SurkaMelee : Enemy
{
    [SerializeField] ParticleSystem AttackTrail;
    [SerializeField] ParticleSystem Slash;
    public override void Start()
    {
        base.Start();
        //damage
        damage = int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaMelee")[2]);
        damage = Mathf.RoundToInt(damage + (GamemasterController.GameMaster.Difficulty * 2));
        //life
        life = int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaMelee")[1]);

    }

    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    public override void Die()
    {
        GamemasterController.GameMaster.AddMoney(int.Parse(GamemasterController.GameMaster.getCsvValues("SurkaMelee")[3]));
        base.Die();
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
}
