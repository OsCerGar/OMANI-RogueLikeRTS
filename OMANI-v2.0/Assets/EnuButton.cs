using UnityEngine;

public class EnuButton : Interactible
{

    bool active;
    EnuSystem enuSystem;
    Animator eggAnimator;

    public override void Start()
    {
        base.Start();

        linkPrice = 20;
        finalLinkPrice = 50;
        price = 250;
        enuSystem = GetComponentInParent<EnuSystem>();
        eggAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (active)
        {
            //Reduce energy 
            ReducePower(2);
            if (powerReduced > 0 && powerReduced < 1)
            {
                powerReduced = 0;
                StopEnuSystem();
                active = false;
            }
        }
        else
        {
            if (Time.time - startTime > 3f && powerReduced > 0)
            {
                StartEnuSystem();
            }
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();

        if (!fullActioned)
        {

            eggAnimator.Play("EGGUP", 0, powerReduced / price);
        }
        else
        {
            //If the animation is almost finished.
            if (latestFullActionPowerReduced > 0.95f)
            {
                base.LateUpdate();

                fullActioned = false;
            }

            latestFullActionPowerReduced = Mathf.Lerp(latestFullActionPowerReduced, 1, 0.01f);
            eggAnimator.Play("EGGUP", 0, latestFullActionPowerReduced);

        }

    }

    public override void Action()
    {
        if (!active)
        {
            base.Action();
        }
    }
    public override void FullAction()
    {
        if (!active)
        {
            base.FullAction();
            fullActioned = true;
        }
    }

    public override void ActionCompleted()
    {
        base.ActionCompleted();
        powerReduced = price;
        StartEnuSystem();
    }

    private void StartEnuSystem()
    {
        enuSystem.startSystem();
        active = true;
    }
    private void StopEnuSystem()
    {
        enuSystem.stopSystem();
    }

}
