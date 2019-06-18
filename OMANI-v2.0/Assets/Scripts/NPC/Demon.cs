using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Demon : Enemy {

    public override void Start()
    {
        base.Start();
        //damage
        damage = int.Parse(GamemasterController.GameMaster.getCsvValues("CorruptedDemon")[2]);
        damage = Mathf.RoundToInt(damage + (GamemasterController.GameMaster.Difficulty * 2));
        //life
        life = int.Parse(GamemasterController.GameMaster.getCsvValues("CorruptedDemon")[1]);

    }

    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }

    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }

}
