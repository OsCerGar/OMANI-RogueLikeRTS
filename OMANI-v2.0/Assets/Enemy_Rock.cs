using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rock : Enemy
{

    BoxCollider rockCollider;
    // Use this for initialization
    public override void Start()
    {
        circle = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        SM = GetComponentInChildren<SoundsManager>();
        anim = this.gameObject.GetComponentInChildren<Animator>();

        if (this.transform.Find("UI") != null)
        {
            if (this.transform.Find("UI/SelectionAnimationParent") != null)
            {
                GUI = this.transform.Find("UI/SelectionAnimationParent").gameObject;
                GUI_Script = this.transform.Find("UI/SelectionAnimationParent").GetComponent<UI_PointerSelection>();
            }
            ui_information = this.transform.Find("UI").gameObject;

        }
        startLife = life;
        rockCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void Die()
    {
        this.enabled = false;
        rockCollider.enabled = false;
    }

    //Simple way to take damage
    public override void TakeDamage(int damage, Color damageType)
    {
        if (state == "Alive")
        {
            if (anim != null)
            {
                anim.SetTrigger("Hitted");
            }
            life -= damage;
            if (life <= 0)
            {
                anim.SetTrigger("Dead");
                state = "Dead";
                Die();
            }
        }
    }
}
