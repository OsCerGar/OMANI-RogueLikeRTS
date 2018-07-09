using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Repeater : Interactible
{
    Animator animator;
    public bool energy = false;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        price = 5;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Action()
    {
        if (energy == false)
        {
            base.Action();
            energy = true;
        }
        else
        {
            StopWorking();
            energy = false;
        }
    }
    public override void ActionCompleted()
    {
        base.ActionCompleted();
        animator.SetBool("Energy", true);
    }

    private void StopWorking()
    {
        animator.SetBool("Energy", false);
    }

}
