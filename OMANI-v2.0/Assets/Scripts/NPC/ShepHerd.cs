using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class ShepHerd : NPC
{
    [HideInInspector]
    public GameObject ResPos;

    private GameObject obj;
    public GameObject Obj
    {
        get
        {
            return obj;
        }

        set
        {
            obj = value;
        }
    }

    public override void  Start()
    {
        base.Start();
        ResPos = transform.Find("ResPos").gameObject;
    }

    void Awake()
    {

        Debug.Log(AI);
        Debug.Log(AI);
        boyType = "Shepherd";
    }
    void Update()
    {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral
        if (state == "Alive")
        {
            if (life <= 0)
            {
                //provisional :D
                Die();
                state = "Dead";
            }
        }

        //Animspeed conected to navmesh speed 
        anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
        if (obj != null)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, ResPos.transform.position,Time.deltaTime);
        }
    }
    public void SetTarget(GameObject trgt)
    {
        var targ = (SharedGameObject)AI.GetVariable("Target");
        targ.Value = trgt;
    }
    public void SetFree()
    {
        var targ = (SharedGameObject)AI.GetVariable("Target");
        targ.Value = null;
    }

    public void Searching()
    {
        var targ = (SharedString)AI.GetVariable("State");
        targ.Value = "SearchRes";
    }

    public void DropObj()
    {
        obj.tag = "Untagged";
        obj.layer = 0;
        obj = null;
    }
}
