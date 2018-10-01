using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_Building_Action : Interactible
{

    [SerializeField]
    BU_UniqueBuilding parentResources;
    public bool readyToSpawn { get; set; }
    Animator animator;
    AudioSource pilarmovement;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        parentResources = this.transform.parent.GetComponent<BU_UniqueBuilding>();

        animator = this.GetComponentInChildren<Animator>();
        linkPrice = 14;
        price = 75;
        finalLinkPrice = 65;
        currentLinkPrice = 0;
        t = 0.2f;
        pilarmovement = this.transform.Find("Sounds").Find("PilarMovement").GetComponent<AudioSource>();
    }

    public void BuildingAction()
    {
        parentResources.BuildingAction();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        if (!animator.GetBool("Energy") && powerReduced < price)
        {
            animator.Play("PilarDown", 0, powerReduced / price);

            if (powerReduced == 0)
            {
                pilarmovement.volume = 0;
            }
            else { pilarmovement.volume = 0.6f; } 
        }
        else
        {
            pilarmovement.volume = 0;
        }
    }

    public override void Action()
    {
        if (readyToSpawn)
        {
            base.Action();
        }
        else if (!animator.GetBool("Energy"))
        {
            animator.SetTrigger("NotReady");
        }
    }

    public override void ActionCompleted()
    {
        BuildingAction();

        base.ActionCompleted();
    }

    public void StopWorkingAnimator()
    {
        animator.SetBool("Energy", true);
    }

}
