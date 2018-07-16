using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_Building_Action : Interactible
{

    BU_UniqueBuilding parentResources;
    private float timeToSpawnEnnui = 45;
    private float timeToSpawnEnnuiCounter, biggestClockValue;
    Image ennuiClocks;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        parentResources = this.transform.parent.GetComponent<BU_UniqueBuilding>();
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

    public override void Action()
    {
        base.Action();
    }

    public override void ActionCompleted()
    {
        BuildingAction();
        base.ActionCompleted();
    }

    private void StopWorking()
    {
    }

}
