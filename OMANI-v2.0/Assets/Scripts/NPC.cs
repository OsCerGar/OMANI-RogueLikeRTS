using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class NPC : MonoBehaviour {

    //Type of NPC
    [SerializeField]
    string boyType = "Swordsman";

    //State on NPC, not to get confused with Behaviour Tree State.  
    [SerializeField]
    string state = "Alive";

    [SerializeField]
    int life, damage;

    //Required for run animations synced with NevMesh
    Animator anim;
    NavMeshAgent Nav;

    private BehaviorTree AI;

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
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


    // Use this for initialization
    void Start () {
        AI = this.gameObject.GetComponent<BehaviorTree>();
        anim = this.gameObject.GetComponent<Animator>();
        Nav = this.gameObject.GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        //He dies if life lowers 
        //TODO : Make this an animation, and make it so that it swaps his layer and tag to something neutral
		if (life <= 0)
        {
            //provisional :D
            Destroy(this.gameObject);
        }
        //Animspeed conected to navmesh speed 
        anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);

	}

    public void Follow(GameObject player) {
        AI.EnableBehavior();
        AI_SetState("Follow");
        AI_SetTarget(player);
    }

    public void Order(GameObject attackPosition)
    {
        AI.EnableBehavior();
        AI_SetState("Attack");
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
