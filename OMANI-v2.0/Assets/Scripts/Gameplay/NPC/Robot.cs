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
    public Robot_Energy robot_energy;

    public float energycap = 100,currentenergy = 0;
    public float powerReduced = 0, linkPrice = 1;
    
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
        dissolveEffect.StartDissolve();
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

    public void BoostAttack()
    {
        var Mattack = Attackzone.GetComponent<MeleAttack>();
        Mattack.ActivateBoostAttack();

    }

    
}
