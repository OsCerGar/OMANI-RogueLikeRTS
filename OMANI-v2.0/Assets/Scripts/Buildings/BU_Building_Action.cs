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

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        parentResources = this.transform.parent.GetComponent<BU_UniqueBuilding>();

        animator = this.GetComponentInChildren<Animator>();
        linkPrice = 3;
        price = 5;
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
        }
    }

    public override void Action()
    {
        if (readyToSpawn)
        {
            base.Action();
        }
        else
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
