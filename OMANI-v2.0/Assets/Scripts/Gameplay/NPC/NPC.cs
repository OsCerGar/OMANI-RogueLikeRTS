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
    public int startLife, life, damage, resurrectCost = 25, powerUpCost = 10;
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
    public GameObject ui_information = null;
    public GameObject Attackzone;

    public bool RootMotion;
    public int peopl;
    [SerializeField] ParticleSystem[] hitEffects;
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
        peopl = LayerMask.NameToLayer("People");
        AI = this.gameObject.GetComponent<BehaviorTree>();
        anim = this.gameObject.GetComponent<Animator>();
        Nav = this.gameObject.GetComponent<NavMeshAgent>();
        circle = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (this.transform.Find("UI") != null)
        {

            ui_information = this.transform.Find("UI").gameObject;

        }
        //Get AttackZone child Somewhere 
        if (transform.FindDeepChild("AttackZone") != null)
        {
            Attackzone = transform.FindDeepChild("AttackZone").gameObject;
        }

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
            }
            else
            {
                disabled = false;
                anim.SetBool("KnockBack", false);
                disabledCountdown = 0;
            }
        }
        GameObject enem = AI_GetEnemy();
        if (enem != null)
        {
            if (enem.GetComponent<NPC>() != null && enem.GetComponent<NPC>().Life <= 0)
            {
                AI_SetEnemy(null);
            }
        }
        

        //Animspeed conected to navmesh speed 
        if (anim != null)
        {
            anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
        }



    }
    //take damage with knockBack
    public void TakeDamage(int damage, bool knockback, float knockbackTime, Transform _perpetrator)
    {
        if (state == "Alive")
        {
            GetHitEffect();
            anim.SetTrigger("Hit");
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
                        disabled = true;
                        disabledTime = knockbackTime;
                        anim.SetBool("KnockBack", true);
                        perpetrator = _perpetrator;
                }
            }


        }

    }
    //Simple way to take damage
    public void TakeDamage(int damage)
    {
        GetHitEffect();
        if (state == "Alive")
        {
            if (anim != null)
            {
                anim.SetTrigger("Hit");
            }
            life -= damage;
            if (life <= 0)
            {
                Die();
                state = "Dead";
            }
        }

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
        life = 0;
        anim.SetTrigger("Die");
        //this.gameObject.GetComponent<Collider>().enabled = false;
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        this.gameObject.layer = 0;
        //this.enabled = false;
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

    public void AttackHit()
    {
        Attackzone.SetActive(true);
    }

    public virtual void Order(GameObject attackPosition)
    {
        AI.EnableBehavior();
        AI_SetState("Attack");
        AI_SetTarget(attackPosition);
    }

    public virtual void ChargedOrder()
    {
        AI.EnableBehavior();
        AI_SetState("SpecialAttack");
    }

    private void GetHitEffect()
    {
        if (hitEffects.Length > 0)
        {
            hitEffects[UnityEngine.Random.Range(0, hitEffects.Length)].Play();
        }
    }
    //Old, remove !?
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

    public virtual void AI_GoToPoint(GameObject _target, float _speed)
    {
        var speed = (SharedGameObject)AI.GetVariable("AnimSpeed");
        var targetVariable = (SharedGameObject)AI.GetVariable("Target");
        targetVariable.Value = _target;

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

    private bool checkAI()
    {
        if (AI != null)
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
        else
        {
            return false;
        }

    }
    void OnAnimatorMove()
    {
        if (RootMotion)
        {
            Nav.velocity = anim.deltaPosition / Time.deltaTime;
        }
    }


}
