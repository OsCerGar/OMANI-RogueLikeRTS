using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Lever : Interactible
{

    Animation animations;
    BU_Resources parent;
    bool left;

    private void Awake()
    {
        animations = this.GetComponentInChildren<Animation>();
        parent = this.transform.parent.GetComponent<BU_Resources>();
    }

    public override void Action(BoyMovement _boy)
    {
        if (left == true)
        {
            Right_Lever();
            left = false;
        }
        else
        {
            Left_Lever();
            left = true;
        }
    }

    public void Left_Lever()
    {
        animations.Play("Lever_left");
        parent.State(true);
    }

    public void Right_Lever()
    {
        animations.Play("Lever_right");
        parent.State(false);
    }
}
