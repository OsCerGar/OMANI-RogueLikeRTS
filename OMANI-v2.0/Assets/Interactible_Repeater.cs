using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Repeater : Interactible
{
    Animator animator;
    [SerializeField]
    public bool energy;
    GameObject top;
    BU_Energy energyBU;
    private float startTimeRepeater = 0, stopTimeRepeater = 0;


    private void Initializer()
    {
        energyBU = this.transform.root.GetComponentInChildren<BU_Energy>();
        animator = this.GetComponent<Animator>();
        top = this.transform.Find("Stick/Top").gameObject;

        linkPrice = 25;
        price = 15;
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
                StopWorking();
            }
        }
        if (powerReduced <= price)
        {
            animator.SetFloat("Blend", powerReduced / price);
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
        base.ActionCompleted();
        if (!energy)
        {
            animator.SetBool("Energy", true);
            energyBU.RequestCable(top);
            energy = true;
            linkPrice = 10;
            price = 2;

        }
        else
        {
            StopWorking();
            energy = false;
            linkPrice = 25;
            price = 15;
        }

        startTimeRepeater = Time.time;
    }

    private void StopWorking()
    {
        energyBU.pullBackCable(top.transform);
        animator.SetBool("Energy", false);
    }
}
