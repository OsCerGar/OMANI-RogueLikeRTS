using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Repeater : Interactible
{

    public Animator animator;
    [SerializeField]
    public bool energy;
    GameObject top;
    BU_Energy energyBU;
    private float startTimeRepeater = 0, stopTimeRepeater = 0;
    private float linkPriceOn, linkPriceOff, priceOn, priceOff;

    private void Initializer()
    {
        energyBU = this.transform.root.GetComponentInChildren<BU_Energy>();
        animator = this.GetComponent<Animator>();
        top = this.transform.Find("Stick/Top").gameObject;

        linkPriceOff = 15;
        linkPriceOn = 25;
        priceOn = 25;
        priceOff = 50;

        linkPrice = linkPriceOff;
        price = priceOff;
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
                StopWorking();
            }
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        if (!energy && powerReduced < price)
        {
            animator.Play("RepeaterUp", 0, powerReduced / price);
        }

    }

    public override void Action()
    {
        if (Time.time - startTimeRepeater > 3)
        {
            if (energyBU != null)
            {
                if (energyBU.energyCheck() || energy)
                {
                    base.Action();
                }
            }
            else
            {
                base.Action();
            }
        }
    }

    public override void ActionCompleted()
    {
        if (!energy)
        {
            energy = true;
            animator.SetBool("Energy", true);
            energyBU.RequestCable(top);
            linkPrice = linkPriceOn;
            price = priceOn;
        }

        else
        {
            StopWorking();
            energy = false;
        }

        base.ActionCompleted();

        startTimeRepeater = Time.time;
    }

    private void StopWorking()
    {
        energyBU.pullBackCable(top.transform);
        animator.SetBool("Energy", false);
        energy = false;
        linkPrice = linkPriceOff;
        price = priceOff;
    }


    //This is for the energy BU
    public void StopWorkingComplete()
    {
        base.ActionCompleted();
        animator.SetBool("Energy", false);
        energy = false;
        linkPrice = linkPriceOff;
        price = priceOff;
    }
}
