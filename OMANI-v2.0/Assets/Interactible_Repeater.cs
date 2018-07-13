using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Repeater : Interactible
{
    Animator animator;
    public bool energy = false;
    GameObject top;
    BU_Energy energyBU;


    // Use this for initialization
    public override void Start()
    {
        base.Start();
        energyBU = this.transform.root.GetComponentInChildren<BU_Energy>();
        price = 5;
        animator = this.GetComponent<Animator>();
        top = this.transform.Find("Stick/Top").gameObject;
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
            if (energyBU != null)
            {
                if (energyBU.RequestCable(top))
                {
                    base.Action();
                    energy = true;
                }
            }
            else
            {
                base.Action();
                energy = true;
            }
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
        energyBU.pullBackCable(top.transform);
        animator.SetBool("Energy", false);
    }

}
