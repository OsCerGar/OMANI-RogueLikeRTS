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
    public float linkPriceOn, linkPriceOff, priceOn, priceOff;

    private void Initializer()
    {
        energyBU = this.transform.root.GetComponentInChildren<BU_Energy>();
        animator = this.GetComponent<Animator>();
        top = this.transform.Find("Stick/Top").gameObject;

        linkPriceOff = 35;
        linkPriceOn = 45;
        priceOn = 25;
        priceOff = 50;

        currentLinkPrice = 0;
        finalLinkPrice = 22;
        t = 0.2f;

        linkPrice = linkPriceOff;
        price = priceOff;
    }

    private void linkPriceChart()
    {
        currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
        t += t * Time.unscaledDeltaTime;
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

        if (animator.GetBool("Ready") && powerReduced < price && !energy)
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
                    linkPriceChart();
                    newAction();
                    //base.Action();
                }
            }
            else
            {
                linkPriceChart();
                newAction();
                //base.Action();
            }
        }
    }

    private void newAction()
    {
        startTime = Time.time;

        if (powers.reducePower(currentLinkPrice))
        {
            laserAudio.energyTransmisionSound(currentLinkPrice);
            powerReduced += currentLinkPrice * Time.unscaledDeltaTime;
            actionBool = true;
        }
        else
        {
            actionBool = false;
        }

    }

    public override void ActionCompleted()
    {
        if (!energy)
        {
            energy = true;
            animator.SetBool("Energy", true);
            animator.SetBool("Ready", false);
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
