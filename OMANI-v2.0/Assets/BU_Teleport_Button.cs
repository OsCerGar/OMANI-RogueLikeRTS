using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Teleport_Button : Interactible
{

    BU_Teleport bu_teleport;

    // Use this for initialization
    public override void Start()
    {
        bu_teleport = this.transform.parent.GetComponent<BU_Teleport>();
    }

    /*
    public override void Action(BoyMovement _boy)
    {
        bu_teleport.StartTeleport();
    }
    */

}
