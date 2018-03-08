using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;

public class NPC : MonoBehaviour {

    #region Variables
    //Type of NPC
    [SerializeField]
    public string boyType;

    //State on NPC, not to get confused with Behaviour Tree State.  
    public string state;

    [SerializeField]
    public int life, damage;
    //Required for run animations synced with NevMesh
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent Nav;

    [HideInInspector]
    public  BehaviorTree AI;
    #endregion

    #region GETTERSETTERS
    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            if (value < life)
            {
                anim.SetTrigger("Hit");
            }
            life = value;

        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public string BoyType
    {
        get
        {
            return boyType;
        }

        set
        {
            boyType = value;
        }
    }

    public string State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }
    #endregion

    // Use this for initialization
    public virtual void Start () {
        AI = this.gameObject.GetComponent<BehaviorTree>();
        anim = this.gameObject.GetComponent<Animator>();
        Nav = this.gameObject.GetComponent<NavMeshAgent>();

        //Nav.updateRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
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

	}

    public void Die()
    {
        AI.enabled = false;
        Nav.enabled = false;
        anim.SetTrigger("Die");
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.tag = "Untagged";
        this.gameObject.layer = 0;
        //cambiar tag y layer
    }

    public void Follow(GameObject player) {
        AI.EnableBehavior();
        AI_SetState("Follow");
        AI_SetTarget(player);
        anim.SetBool("SpecialAttack",false);
    }

    public void Order(GameObject attackPosition)
    {
        AI.EnableBehavior();
        AI_SetState("Attack");
        AI_SetTarget(attackPosition);
    }

    public void ChargedOrder(GameObject attackPosition)
    {
        AI.EnableBehavior();
        AI_SetState("SpecialAttack");
        AI_SetTarget(attackPosition);
    }

    public void ChargedOrderFullfilled(GameObject attackPosition)
    {
        AI.EnableBehavior();

        var stateVariable = (SharedBool)AI.GetVariable("Go");
        stateVariable.Value = true;
        AI_SetTarget(attackPosition);
        
    }

    public void AI_SetState(string state) {
        var stateVariable = (SharedString)AI.GetVariable("State");
        stateVariable.Value = state;
    }

    public void AI_SetTarget(GameObject target)
    {
        var targetVariable = (SharedGameObject)AI.GetVariable("Target");
        targetVariable.Value = target;
    }

    public string AI_GetState()
    {
        var stateVariable = (SharedString)AI.GetVariable("State");
        return stateVariable.Value;
    }

    public GameObject AI_GetTarget()
    {
        var targetVariable = (SharedGameObject)AI.GetVariable("Target");
        return targetVariable.Value;
    }

}
