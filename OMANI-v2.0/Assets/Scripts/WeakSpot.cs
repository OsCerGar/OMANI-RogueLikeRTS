using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpot : Enemy {

    WeakSpotBrain brain;
    private void Awake()
    {

       brain = transform.parent.GetComponent<WeakSpotBrain>();
    }
    public override void Die()
    {
        brain.RemoveWeakSpot(transform.gameObject);
    }
    private void OnEnable()
    {
        if (life<startLife)
        {
            life = startLife;
            state = "Alive";
        }
    }
}
