using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : NPC
{
    bool link = false;
    Link linky;
    Powers powers = null;
    PowerManager powerManager;
    DissolveEffectController dissolveEffect;

    public float powerReduced = 0, linkPrice = 1;

    public void StartResurrection()
    {
        anim.SetTrigger("GetUp");
        dissolveEffect.StartRevert();
    }

    public override void Start()
    {
        base.Start();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        dissolveEffect = GetComponentInChildren<DissolveEffectController>();

    }

    public override void Update()
    {
        base.Update();

        //Powers Link
        if (link)
        {
            if (powers.reducePower(linkPrice * Time.unscaledDeltaTime))
            {
                powerReduced += linkPrice * Time.unscaledDeltaTime;
            }

            else
            {
                DestroyLink();
            }

            if (state == "Alive")
            {
                if (powerReduced >= powerUpCost)
                {
                    ActionCompleted();
                    //PowerUP
                    BoostAttack();
                }
            }
            else
            {
                if (powerReduced >= resurrectCost)
                {
                    ActionCompleted();
                    StartResurrection();
                }
            }
        }

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

    public virtual void Action()
    {
        if (!link)
        {
            CreateLink();
        }
    }

    public virtual void ActionCompleted()
    {
        linky.Completed();
        link = false;
        powerReduced = 0;
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
