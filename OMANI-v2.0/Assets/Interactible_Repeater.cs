using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Repeater : Interactible
{
    Animator animator;
    public bool energy { get; set; }
    GameObject top;
    BU_Energy energyBU;
    private int repeaterPrice = 15;


    private void Initializer()
    {
        energyBU = this.transform.root.GetComponentInChildren<BU_Energy>();
        animator = this.GetComponent<Animator>();
        top = this.transform.Find("Stick/Top").gameObject;

        linkPrice = 10;
        price = repeaterPrice;
    }
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        Initializer();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (energyBU != null)
        {
            if (!energy)
            {
                energyBU.pullBackCable(top.transform);
                //DestroyLink();
                StopWorking();
            }
        }
    }

    public override void Action()
    {
        if (!energy)
        {
            if (energyBU != null)
            {
                if (energyBU.energyCheck())
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
        energyBU.RequestCable(top);
    }

    private void StopWorking()
    {
        energyBU.pullBackCable(top.transform);
        animator.SetBool("Energy", false);
    }

}
