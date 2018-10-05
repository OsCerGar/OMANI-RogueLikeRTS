using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Robot : NPC
{
    bool link = false;
    Link linky;
    Powers powers = null;
    PowerManager powerManager;
    DissolveEffectController dissolveEffect;
    Army commander;
    public Robot_Energy robot_energy;


    public void StartResurrection()
    {
        anim.SetTrigger("GetUp");
        dissolveEffect.StartRevert();
    }
    public override void Start()
    {
        base.Start();
        robot_energy = this.transform.GetComponent<Robot_Energy>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        dissolveEffect = GetComponentInChildren<DissolveEffectController>();
        commander = FindObjectOfType<Army>();
    }
    public override void Update()
    {
        base.Update();
        //DisablesCircle when given an order.
        if (state != "Alive")
        {
            Debug.Log("Dead");
            GUI_Script.DisableCircle();
        }

    }
    public override void Die()
    {
        base.Die();
        //dissolveEffect.StartDissolve();
    }
    public void Resurrect()
    {

        Nav.updatePosition = true;
        Nav.updateRotation = true;
        life = startLife;
        //this.gameObject.GetComponent<Collider>().enabled = true;
        this.gameObject.GetComponent<Collider>().isTrigger = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.layer = peopl;
        state = "Alive";
        //cambiar tag y layer
    }

    public void AutoReclute()
    {
        commander.Reclute(this);
    }

    private void CreateLink()
    {
        //CreatesLink
        linky = powerManager.CreateLink(this.transform, powers).GetComponent<Link>();

        linky.power = powers.gameObject;
        linky.interactible = this.transform.gameObject;
        link = true;
    }

    private void DestroyLink()
    {
        linky.Failed();
        link = false;
        powerReduced = 0;
    }
    

}
