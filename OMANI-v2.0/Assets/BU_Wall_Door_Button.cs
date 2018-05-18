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

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action(BoyMovement _boy)
    {
        door.DoorUp();
    }

}
