using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Wall_Door : MonoBehaviour
{

    Animation door;

    //false = down, true = up
    bool state = false;

    // Use this for initialization
    void Start()
    {
        door = this.GetComponent<Animation>();
    }

    public void DoorUp()
    {
        door.Play("Door_up");
    }

    public void DoorDown()
    {
        door.Play("Door_down");
    }
}
