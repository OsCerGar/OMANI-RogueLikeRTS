using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Wall_Door_Button : Interactible
{

    BU_Wall_Door door;

    // Use this for initialization
    private void Awake()
    {
        door = this.transform.parent.GetComponent<BU_Wall_Door>();
    }

    /*
    public override void Action(BoyMovement _boy)
    {
        if (door.state == false)
        {
            door.DoorUp();
        }
        else if (door.doorCounter > 3.5f)
        {
            door.DoorDown();
        }
    }
    */
}
