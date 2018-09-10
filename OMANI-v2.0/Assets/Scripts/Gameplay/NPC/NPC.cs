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
    public int startLife, life, damage, resurrectCost = 25, powerUpCost = 10, increaseAmount = 1;
    public float maxpowerPool = 5, powerPool = 0;
    public float powerReduced = 0, linkPrice = 1;
    int quarter, half, quartandhalf;

    float sumAmount, lastLife;
    int lifeQuarter, lifeHalf, lifeQuarterAndHalf;



    //Required for run animations synced with NevMesh
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent Nav;

    [HideInInspector]
    public SpriteRenderer circle;

    [HideInInspector]
    public BehaviorTree[] AllBehaviour;
    public BehaviorTree IdleTree, FollowTree, AttackTree, GoTree, CoolDownTree;

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
    [SerializeField]
    public GameObject GUI;
    public UI_PointerSelection GUI_Script;
    [SerializeField] ParticleSystem[] hitEffects;
    [HideInInspector]public SoundsManager SM;

    UI_RobotAttack UI_Attack;
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



    public void GUI_Activate()
    {
        GUI.SetActive(true);
    }
    public void GUI_Disable()
    {
        GUI.SetActive(false);
        Debug.Log("Disabled by that");
    }

    // Use this for initialization
    public virtual void Start()
    {
        SM = GetComponentInChildren<SoundsManager>();
        peopl = LayerMask.NameToLayer("People");
        //We get all behaviourTrees
        AllBehaviour = GetComponents<BehaviorTree>();
        foreach (var item in AllBehaviour)
        {
            if (item.BehaviorName == "Idle")
            {
                IdleTree = item;
            }
            if (item.BehaviorName == "Follow")
            {
                FollowTree = item;
            }
            if (item.BehaviorName == "Attack")
            {
                AttackTree = item;
            }
            if (item.BehaviorName == "Go")
            {
                GoTree = item;
            }
            if (item.BehaviorName == "CoolDown")
            {
                CoolDownTree = item;
            }
        }
        enableTree("Idle");
        anim = this.gameObject.GetComponent<Animator>();
        Nav = this.gameObject.GetComponent<NavMeshAgent>();
        circle = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (this.transform.Find("UI") != null)
        {
            if (this.transform.Find("UI/SelectionAnimationParent") != null)
            {
                GUI = this.transform.Find("UI/SelectionAnimationParent").gameObject;
                GUI_Script = this.transform.Find("UI/SelectionAnimationParent").GetComponent<UI_PointerSelection>();
            }
            ui_information = this.transform.Find("UI").gameObject;

        }
        //Get AttackZone child Somewhere 

        startLife = life;
        //Nav.updateRotation = true;

        UI_Attack = GetComponentInChildren<UI_RobotAttack>();

        quarter = Mathf.RoundToInt(maxpowerPool * 0.25f);
        half = Mathf.RoundToInt(maxpowerPool * 0.5f);
        quartandhalf = Mathf.RoundToInt(maxpowerPool * 0.75f);


    }
    public void AttackSound()
    {
        SM.AttackHit();
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
            if (!RootMotion)
            {
                anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
            }
        }

        EnergyLifeCalc();

    }

    private void EnergyLifeCalc()
    {
        #region ReducePowerPool

        if (powerPool < quarter)
        {
            powerPool = Mathf.Clamp(powerPool, 0, quarter);
        }
        else if (powerPool < half)
        {
            powerPool = Mathf.Clamp(powerPool, quarter, half);
        }
        else if (powerPool < quartandhalf)
        {
            powerPool = Mathf.Clamp(powerPool, half, quartandhalf);
        }
        else if (powerPool <= maxpowerPool || powerPool > maxpowerPool)
        {
            powerPool = Mathf.Clamp(powerPool, quartandhalf, maxpowerPool);
        }
        #endregion
        #region IncreaseLifePool
        if (life < lifeQuarter)
        {
            sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
            if (sumAmount > 1)
            {
                life = Mathf.Clamp(life + 1, 0, lifeQuarter);
            }
        }
        else if (life < lifeHalf)
        {
            sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
            if (sumAmount > 1)
            {
                life = Mathf.Clamp(life + 1, 0, lifeHalf);
            }
        }
        else if (life < lifeQuarterAndHalf)
        {
            sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
            if (sumAmount > 1)
            {
                life = Mathf.Clamp(life + 1, 0, lifeQuarterAndHalf);
            }
        }
        else if (life < startLife || life > startLife)
        {
            sumAmount += life + increaseAmount * Time.unscaledDeltaTime;
            if (sumAmount > 1)
            {
                life = Mathf.Clamp(life + 1, 0, startLife);
            }
        }
        sumAmount = 0;
        #endregion
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

                Debug.Log("buscando el wtf");
                AI_SetEnemy(null);
            }
        }
    }

    public virtual void Die()
    {
        if (AllBehaviour != null)
        {
            foreach (var item in AllBehaviour)
            {
                item.DisableBehavior(false);
            }
        }
        life = 0;
        anim.SetTrigger("Die");

        Nav.updatePosition = false;
        Nav.updateRotation = false;
        Nav.isStopped = true;
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        this.gameObject.layer = 0;
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

    public virtual void Follow(GameObject _position)
    {
        enableTree("Follow");
        var stateVariable = (SharedGameObject)FollowTree.GetVariable("Position");
        stateVariable.Value = _position;
    }
    public virtual void Idle()
    {
        IdleTree.EnableBehavior();
    }

    public virtual void AttackHit()
    {
        Attackzone.SetActive(true);
        reducePowerNow(maxpowerPool);
        enableTree("CoolDown");
    }

    public bool reducePower(float amount)
    {
        float finalAmount = amount * Time.unscaledDeltaTime;
        if (powerPool - finalAmount >= 0)
        {
            powerPool -= finalAmount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool reducePowerNow(float amount)
    {
        if (powerPool - amount >= 0)
        {
            powerPool -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual void Order(GameObject attackPosition)
    {
        enableTree("Go");
        var stateVariable = (SharedGameObject)GoTree.GetVariable("Position");
        stateVariable.Value = attackPosition;
        //set information here
    }
    public virtual void Attack(GameObject attackPosition)
    {
        var stateVariable = (SharedGameObject)AttackTree.GetVariable("Enemy");
        stateVariable.Value = attackPosition;
        enableTree("Attack");
        //set information here
    }
    private void GetHitEffect()
    {
        if (hitEffects.Length > 0)
        {
            hitEffects[UnityEngine.Random.Range(0, hitEffects.Length)].Play();
        }
    }

    /*
    public virtual void AI_SetState(string state)
    {
        var stateVariable = (SharedString)AI.GetVariable("State");
        stateVariable.Value = state;
    }
    public virtual void AI_SetTarget(GameObject target)
    {
        if (AI != null)
        {
            var targetVariable = (SharedGameObject)AI.GetVariable("Target");
            targetVariable.Value = target;
        }
    }
    */

        
    public virtual void AI_SetEnemy(GameObject target)
    {

        var targetVariable = (SharedGameObject)GetComponent<BehaviorTree>().GetVariable("Enemy");
        targetVariable.Value = target;


    }
    public virtual GameObject AI_GetEnemy()
    {

        var targetVariable = (SharedGameObject)GetComponent<BehaviorTree>().GetVariable("Enemy");
        return targetVariable.Value;


    }
    public virtual GameObject AI_GetTarget()
    {
        var targetVariable = (SharedGameObject)GetComponent<BehaviorTree>().GetVariable("Target");
        return targetVariable.Value;
    }
    /*
    public virtual void Mutate(GameObject _mutation)
    {
        GameObject mutant = Instantiate(_mutation, this.transform.position, this.transform.rotation);

        if (AI_GetState() == "Follow")
        {
            FindObjectOfType<Army>().RemoveFromList(this);
        }

        Destroy(this.gameObject);

    }
    */
    public void enableTree(string _name)
    {
        foreach (var item in AllBehaviour)
        {
            if (item.BehaviorName == _name)
            {
                item.EnableBehavior();
            }
            else
            {
                item.DisableBehavior();
            }
        }
    }
    void OnAnimatorMove()
    {
        if (RootMotion)
        {
            // Update position based on animation movement using navigation surface height
            Vector3 position = anim.rootPosition;
            position.y = Nav.nextPosition.y;
            transform.position = position;
            Nav.nextPosition = transform.position;

        }

    }
    public string getState()
    {
        foreach (BehaviorTree item in AllBehaviour)
        {
            if (item.isActiveAndEnabled)
            {
                return item.BehaviorName;
            }
        }
        return null;
    }
    public void ShowAttackUI(GameObject Enemy)
    {
        UI_Attack.Show(Enemy);
    }

}
