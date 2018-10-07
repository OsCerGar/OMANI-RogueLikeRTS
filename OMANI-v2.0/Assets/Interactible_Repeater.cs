using System.Collections.Generic;
using UnityEngine;

public class Interactible_Repeater : Interactible
{
    [HideInInspector]
    public Animator animator;
    public int energy;
    public GameObject top;
    BU_Energy energyBU;
    private float startTimeRepeater = 0;
    private readonly float stopTimeRepeater = 0;
    [HideInInspector]
    public float linkPriceOn, linkPriceOff, priceOn, priceOff;

    //CitySystem
    public List<Interactible_Repeater> closeRepeaters;
    [SerializeField]
    private bool available;
    private GameObject repeaterUI;

    private void Initializer()
    {
        energyBU = transform.root.GetComponentInChildren<BU_Energy>();
        animator = GetComponent<Animator>();
        if (transform.Find("UI") != null)
        {
            repeaterUI = transform.Find("UI").gameObject;
            repeaterUI.SetActive(false);
        }
        top = transform.Find("Stick/Top").gameObject;

        linkPriceOff = 15;
        linkPriceOn = 20;
        priceOn = 25;
        priceOff = 50;

        currentLinkPrice = 0;
        finalLinkPrice = 45;
        t = 0.2f;

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
            if (energy < 1)
            {
                StopWorking();
            }
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        //Plays the up animation when ready
        if (animator.GetBool("Ready") && powerReduced < price && energy < 1)
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
                if (energyBU.energyCheck() || energy > 0)
                {
                    if (energyBU.checkIfLastRepeater(this))
                    {
                        base.Action();
                    }

                    else if (available && energy == 0)
                    {
                        base.Action();
                    }
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
        if (energy < 1)
        {
            energy = 1;
            animator.SetBool("Energy", true);
            animator.SetBool("Ready", false);
            energyBU.RequestCable(top, this);
            linkPrice = linkPriceOn;
            price = priceOn;
        }

        else
        {
            StopWorking();
            energy = 0;
        }

        base.ActionCompleted();

        startTimeRepeater = Time.time;
    }
    private void StopWorking()
    {
        energyBU.pullBackCable(top.transform);
        animator.SetBool("Energy", false);
        energy = 0;
        linkPrice = linkPriceOff;
        price = priceOff;
    }
    //This is for the energy BU
    public void StopWorkingComplete()
    {
        base.ActionCompleted();
        animator.SetBool("Energy", false);
        energy = 0;
        linkPrice = linkPriceOff;
        price = priceOff;
    }

    public void Availablity(bool _value)
    {
        available = _value;
    }

    public void closeRepeatersOnOff(bool _value)
    {
        foreach (Interactible_Repeater repeater in closeRepeaters)
        {
            repeater.Availablity(_value);
        }

        if (repeaterUI != null)
        {
            if (_value) { repeaterUI.SetActive(true); }
            else { repeaterUI.SetActive(false); }
        }
    }
}
