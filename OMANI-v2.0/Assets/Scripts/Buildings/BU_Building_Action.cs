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

        if (powerReduced <= price && animator != null)
        {
            animator.SetFloat("Blend", (powerReduced / price));
        }
    }

    public override void Action()
    {
        if (readyToSpawn)
        {
            base.Action();
        }
    }

    public override void ActionCompleted()
    {
        BuildingAction();
        base.ActionCompleted();
    }

    public void StopWorkingAnimator()
    {
        animator.SetTrigger("Stop");
    }

}
