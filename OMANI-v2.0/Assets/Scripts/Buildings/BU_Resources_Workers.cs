using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Resources_Workers : Interactible
{

    BU_WorkerMaker parentResources;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        parentResources = this.transform.parent.GetComponent<BU_WorkerMaker>();
        price = 5;
    }
    public void StartWorker()
    {
        parentResources.MakeWorker();
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
        StartWorker();
        base.ActionCompleted();
    }

    private void StopWorking()
    {
    }

}
