using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;

public class NPC : MonoBehaviour
{

    #region Variables
    //Type of NPC
    [SerializeField]
    public string boyType;

    //State on NPC, not to get confused with Behaviour Tree State.  
    public string state;

    [SerializeField]
    public int startLife, life, damage;
    //Required for run animations synced with NevMesh
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent Nav;

    [HideInInspector]
    public SpriteRenderer circle;

    [HideInInspector]
    public BehaviorTree AI;

    //Variables for when disabled (knockback)
    bool disabled;
    float disabledTime = 2, disabledCountdown = 0;
    bool KnockBackParabola = false;
    float k;
    Vector3 LandingPosition, initialPosition;
    Transform perpetrator;
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
                if (anim != null)
                {
                    // anim.SetTrigger("Hit");
                }

                life = value;
            }

            else
            {

                life = value;
            }

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
    public virtual void Start()
    {
        AI = this.gameObject.GetComponent<BehaviorTree>();
        anim = this.gameObject.GetComponent<Animator>();
        Nav = this.gameObject.GetComponent<NavMeshAgent>();
        circle = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        startLife = life;
        //Nav.updateRotation = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //He dies if life lowers 

        if (disabled)
        {
            if (disabledCountdown < disabledTime)
            {
                disabledCountdown += Time.deltaTime;
            }else
            {
                disabled = false;
                anim.SetBool("KnockBack",false);
            }
        }
        if (KnockBackParabola)
        {
            if (k<0.93f) {
            k += Time.deltaTime;
            transform.position = MathParabola.Parabola(initialPosition, LandingPosition, 2, k );
            }
            else
            {
                k = 0;
                KnockBackParabola = false;
            }
        }


        //Animspeed conected to navmesh speed 
        if (anim != null)
        {
            anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
        }

    }
    //take damage with knockBack
    public void TakeDamage(int damage, bool knockback,float knockbackTime,Transform _perpetrator)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
            state = "Dead";
        }
        else
        {
            if (knockback)
            {
                if (!disabled)
                {
                    disabledTime = knockbackTime;
                    anim.SetBool("KnockBack", true);
                    DisableNPC();
                    perpetrator = _perpetrator;
                }
            }
        }
        
    }
    public void EnableNPC()
    {
        disabled = false;
        if (AI != null)
        {
            AI.enabled = true;
        }
        Nav.enabled = true;
    }
    public void DisableNPC()
    {
        disabled = true;
        if (AI != null)
        {
            AI.enabled = false;
        }
        Nav.enabled = false;

    }
    //Simple way to take damage
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
            state = "Dead";
        }

    }
    public void KnockBack()
    {
        Vector3 tempLandingPosition;
        if (perpetrator != null)
        {
            transform.LookAt(perpetrator);
            tempLandingPosition = transform.position + perpetrator.forward * 3;
        } else
        {
            tempLandingPosition = transform.forward * -2;
        }
        LandingPosition = new Vector3(tempLandingPosition.x, tempLandingPosition.y+1, tempLandingPosition.z);
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        k = 0;
        KnockBackParabola = true;

    }

    protected void checkVariables()
    {
        //If it's not null 
        if (AI_GetEnemy() != null)
        {
            //But not active
            if (!AI_GetEnemy().activeSelf)
            {
                //Turn it null, and set and stop your order
                AI_SetEnemy(null);
                AI_SetState("Free");
            }
        }
        //Same with target
        if (AI_GetTarget() != null)
        {
            if (!AI_GetTarget().activeSelf)
            {
                AI_SetTarget(null);
                AI_SetState("Free");
            }
        }
    }

    public virtual void Die()
    {
        if (AI != null)
        {
            AI.enabled = false;
            Nav.enabled = false;
        }
        anim.SetTrigger("Die");
        this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.tag = "Untagged";
        this.gameObject.layer = 0;
        this.enabled = false;
        //cambiar tag y layer
    }

    public virtual void Heal(int _heal)
    {
        Debug.Log("Healed " + _heal);
        if (life + _heal > startLife)
        {
            life = startLife;
        }
        else
        {
            life = life + _heal;
        }
    }

    public virtual void Follow(GameObject player)
    {
        AI.EnableBehavior();
        AI_SetState("Follow");
        AI_SetTarget(player);
        anim.SetBool("SpecialAttack", false);
    }

    public virtual void Order(GameObject attackPosition)
    {
        AI.EnableBehavior();
        AI_SetState("Attack");
        AI_SetTarget(attackPosition);
    }

    public virtual void ChargedOrder(GameObject attackPosition)
    {
        AI.EnableBehavior();
        AI_SetState("SpecialAttack");
        AI_SetTarget(attackPosition);
    }

    public virtual void ChargedOrderFullfilled(GameObject attackPosition)
    {
        AI.EnableBehavior();

        var stateVariable = (SharedBool)AI.GetVariable("Go");
        stateVariable.Value = true;
        AI_SetTarget(attackPosition);

    }

    public virtual void AI_SetState(string state)
    {
        var stateVariable = (SharedString)AI.GetVariable("State");
        stateVariable.Value = state;
    }

    public virtual void AI_SetTarget(GameObject target)
    {
        var targetVariable = (SharedGameObject)AI.GetVariable("Target");
        targetVariable.Value = target;
    }

    public virtual string AI_GetState()
    {
        var stateVariable = (SharedString)AI.GetVariable("State");
        return stateVariable.Value;
    }
    public virtual void AI_SetEnemy(GameObject target)
    {
        if (AI != null)
        {
            var targetVariable = (SharedGameObject)AI.GetVariable("Enemy");
            targetVariable.Value = target;
        }

    }
    public virtual GameObject AI_GetEnemy()
    {
        if (checkAI())
        {
            var targetVariable = (SharedGameObject)AI.GetVariable("Enemy");
            return targetVariable.Value;
        }
        else
        {
            return null;
        }
        
    }
    public virtual GameObject AI_GetTarget()
    {
        var targetVariable = (SharedGameObject)AI.GetVariable("Target");
        return targetVariable.Value;
    }



    public virtual void Mutate(GameObject _mutation)
    {
        GameObject mutant = Instantiate(_mutation, this.transform.position, this.transform.rotation);

        if (AI_GetState() == "Follow")
        {
            FindObjectOfType<Army>().RemoveFromList(this);
        }

        Destroy(this.gameObject);

    }

    public virtual void EnableCircle()
    {
        if (this.CompareTag("Enemy"))
        {
            circle.enabled = true;

            circle.material.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(50));
        }

        else if (AI_GetState() == "Follow")
        {
            circle.enabled = true;

            circle.material.SetColor("_EmissionColor", Color.green * Mathf.LinearToGammaSpace(50));
        }

        else
        {
            circle.enabled = true;

            circle.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace(50));
        }
    }

    public virtual void EnablePriorityCircle()
    {
        circle.enabled = true;

        circle.material.SetColor("_EmissionColor", new Color(1, .3f, .1f, .4f) * Mathf.LinearToGammaSpace(50));

    }


    public virtual void DisableCircle()
    {
        if (this.CompareTag("Enemy") || AI_GetState() != "Follow")
        {
            circle.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace(50));
            circle.enabled = false;
        }
    }

    public virtual void DisablePriorityCircle()
    {
        circle.material.SetColor("_EmissionColor", Color.green * Mathf.LinearToGammaSpace(50));
    }

        
    

    private bool checkAI()
{
    if (AI.isActiveAndEnabled)
    {
        return true;
    }
    else
    {
        var allAI = GetComponents<BehaviorTree>();
        foreach (BehaviorTree item in allAI)
        {
            if (item.isActiveAndEnabled)
            {
                AI = item;

                return true;
            }
        }
        return false;
    }
}


}
