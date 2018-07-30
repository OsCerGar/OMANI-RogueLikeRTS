using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {

    public override void Die()
    {
        base.Die();
        Destroy(AI);
        this.enabled = false;
    }
}
