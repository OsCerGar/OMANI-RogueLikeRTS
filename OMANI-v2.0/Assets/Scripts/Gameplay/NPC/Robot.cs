using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : NPC
{
   public void StartResurrection()
    {
        anim.SetTrigger("GetUp");
    }
    public void Resurrect()
    {

        if (AI != null)
        {
            AI.enabled = true;
            Nav.enabled = true;
        }
        life = startLife;
        //this.gameObject.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.layer = peopl;
        state = "Alive";
        //cambiar tag y layer
    }
}
