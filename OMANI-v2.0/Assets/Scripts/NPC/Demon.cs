using BehaviorDesigner.Runtime;
using UnityEngine;

public class Demon : Enemy
{

    public override void Start()
    {
        base.Start();
        //damage
        //damage = int.Parse(GamemasterController.GameMaster.getCsvValues("CorruptedDemon")[2]);
        //damage = Mathf.RoundToInt(damage + (GamemasterController.GameMaster.Difficulty * 2));
        //life
        //life = int.Parse(GamemasterController.GameMaster.getCsvValues("CorruptedDemon")[1]);

    }

    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    public override void Die()
    {
        //GamemasterController.GameMaster.AddMoney(int.Parse(GamemasterController.GameMaster.getCsvValues("CorruptedDemon")[3]));
        GamemasterController.GameMaster.AddMoney(25);
        base.Die();
    }
    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }

}
