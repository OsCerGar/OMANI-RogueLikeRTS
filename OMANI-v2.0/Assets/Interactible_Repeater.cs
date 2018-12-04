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
    private float actionDone;
    //CitySystem
    public List<Interactible_Repeater> closeRepeaters;
    [SerializeField]
    private bool available;
    private GameObject repeaterUI;
    private AudioSource RepeaterLoop;

    private void Initializer()
    {
        energyBU = transform.root.GetComponentInChildren<BU_Energy>();
        animator = GetComponent<Animator>();
        RepeaterLoop = transform.Find("Sounds").Find("UpDown").GetComponent<AudioSource>();
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
        if (!fullActioned)
        {
            //Plays the up animation when ready
            if (animator.GetBool("Ready") && powerReduced < price && energy < 1)
            {
                animator.Play("RepeaterUp", 0, powerReduced / price);

                if (Time.time - actionDone > 0.1f)
                {
                    if (powerReduced > 1f)
                    {
                        RepeaterLoop.volume = 0.05f;
                        RepeaterLoop.pitch = 0.6f;
                    }
                    else
                    {
                        RepeaterLoop.volume = 0f;
                    }
                }
            }

            base.LateUpdate();
        }
        else
        {
            //If the animation is almost finished.
            if (latestFullActionPowerReduced > 0.95f)
            {
                base.LateUpdate();
                fullActioned = false;
            }
            latestFullActionPowerReduced = Mathf.Lerp(latestFullActionPowerReduced, 1, 0.08f);
            animator.Play("RepeaterUp", 0, latestFullActionPowerReduced);

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
                    actionDone = Time.time;
                    //Only Lets you disable the last one.
                    if (energyBU.checkIfLastRepeater(this))
                    {
                        base.Action();
                        RepeaterLoop.pitch = 1f;

                        RepeaterLoop.volume = 0.5f;

                    }

                    else if (available && energy == 0)
                    {
                        base.Action();
                        RepeaterLoop.pitch = 1f;

                        RepeaterLoop.volume = 0.5f;
                    }
                }
            }
            else
            {
                base.Action();
                RepeaterLoop.volume = 0.3f;

            }
        }
    }

    public override void FullAction()
    {
        if (energyBU.energyCheck() || energy > 0)
        {
            if (energyBU.checkIfLastRepeater(this))
            {
                base.FullAction();
            }

            else if (available && energy == 0)
            {
                base.FullAction();

                if (energy < 1)
                {
                    fullActioned = true;
                }
            }
        }
    }

    public override void ActionCompleted()
    {
        RepeaterLoop.volume = 0;
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

    public int getEnergy()
    {
        return energy;
    }
    //Ennu animation 
    public void EnnuAnimation()
    {
        animator.SetTrigger("SendEnergy");
    }
}
