using BehaviorDesigner.Runtime;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NPC : MonoBehaviour
{

    #region Variables
    //Type of NPC
    [SerializeField]
    public string boyType;

    [HideInInspector]
    //State on NPC, not to get confused with Behaviour Tree State.  
    public string state;

    [SerializeField]
    public int startLife, life, damage, powerUpCost = 10;
    [SerializeField] public float maxpowerPool = 5, powerPool = 0, increaseAmount = 0.15f, lifeToHeal = 0;
    [HideInInspector] public float powerReduced = 0, linkPrice = 1;
    [HideInInspector] public int quarter, half, quarterAndHalf;

    [HideInInspector] public float sumAmount, lastLife;
    [HideInInspector] public int lifeQuarter, lifeHalf, lifeQuarterAndHalf;


    // DAMAGE More useless variables
    float acumulatedDamage;

    //Required for run animations synced with NevMesh
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public NavMeshAgent Nav;

    [HideInInspector]
    public SpriteRenderer circle;

    [HideInInspector]
    public BehaviorTree[] AllBehaviour;
    [HideInInspector]
    public BehaviorTree IdleTree, FollowTree, AttackTree, GoTree, CoolDownTree, DeployTree;

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
    [SerializeField] public ParticleSystem[] hitEffects;
    [SerializeField] public Renderer Renderer;
    [HideInInspector] public SoundsManager SM;
    public ThirdPersonCharacter TPC;

    private UI_RobotAttack uI_Attack;

    //UI
    [HideInInspector]
    public NumberPool numberPool;
    [HideInInspector]
    public Transform numbersTransform;
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

    internal UI_RobotAttack UI_Attack
    {
        get
        {
            return uI_Attack;
        }

        set
        {
            uI_Attack = value;
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
        TPC = GetComponent<ThirdPersonCharacter>();
        SM = GetComponentInChildren<SoundsManager>();
        peopl = LayerMask.NameToLayer("People");
        //We get all behaviourTrees
        SetTrees();
        anim = gameObject.GetComponent<Animator>();
        Nav = gameObject.GetComponent<NavMeshAgent>();
        circle = gameObject.GetComponentInChildren<SpriteRenderer>();
        if (transform.Find("UI") != null)
        {
            numbersTransform = transform.Find("UI").Find("Numbers");

            if (transform.Find("UI/SelectionAnimationParent") != null)
            {
                GUI = transform.Find("UI/SelectionAnimationParent").gameObject;
                GUI_Script = transform.Find("UI/SelectionAnimationParent").GetComponent<UI_PointerSelection>();
            }
            ui_information = transform.Find("UI").gameObject;

        }
        //Get AttackZone child Somewhere 

        startLife = life;
        if (Nav != null)
        {
            Nav.updateRotation = false;
            // Nav.updatePosition = false;
        }
        numberPool = FindObjectOfType<NumberPool>();
        UI_Attack = GetComponentInChildren<UI_RobotAttack>();

        quarter = Mathf.RoundToInt(maxpowerPool * 0.25f);
        half = Mathf.RoundToInt(maxpowerPool * 0.5f);
        quarterAndHalf = Mathf.RoundToInt(maxpowerPool * 0.75f);
    }

    virtual public void SetTrees()
    {
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
            if (item.BehaviorName == "Deploy")
            {
                DeployTree = item;
            }
        }
        enableTree("Idle");
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



        EnergyLifeCalc();
        if (Nav != null)
        {
            if (Nav.remainingDistance > 0.6f)
            {

                TPC.Move(Nav.desiredVelocity);
            }
            else
            {
                TPC.Move(transform.position);
            }
        }

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
        else if (powerPool < quarterAndHalf)
        {
            powerPool = Mathf.Clamp(powerPool, half, quarterAndHalf);
        }
        else if (powerPool <= maxpowerPool || powerPool > maxpowerPool)
        {
            powerPool = Mathf.Clamp(powerPool, quarterAndHalf, maxpowerPool);
        }
        #endregion

    }

    //take damage with knockBack
    public virtual void TakeDamage(int damage, bool knockback, float knockbackTime, Transform _perpetrator)
    {
        numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject, false);

        if (state == "Alive")
        {

            StartCoroutine(gotHit());
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
    public virtual void TakeDamage(int damage, Color damageType)
    {
        numberPool.NumberSpawn(numbersTransform, damage, damageType, numbersTransform.gameObject, false);


        StartCoroutine(gotHit());
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

    //Max damage is decided by the laser, in case of future upgrades to the Laser. 
    public virtual void TakeWeakLaserDamage(float _laserSpeed, int _Damage)
    {
        if (state == "Alive")
        {
            acumulatedDamage += (_laserSpeed * Time.unscaledDeltaTime) * _Damage;

            if (acumulatedDamage > _Damage / _laserSpeed)
            {
                TakeDamage(Mathf.RoundToInt(_Damage / _laserSpeed), Color.white);
                //Reset
                acumulatedDamage = 0;
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


        //audio
        SM.Die();
        Nav.isStopped = true;
        Nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    public virtual void Heal(int _heal)
    {
        if (life + _heal > startLife)
        {
            life = startLife;
        }
        else
        {
            life = life + _heal;
        }
    }

    public virtual void slowPowerpoolHeal(float _lerpTime)
    {
        powerPool = Mathf.Lerp(powerPool, maxpowerPool, _lerpTime);

    }
    public void SetTreeVariable(GameObject _variable, string _variableName)
    {
        var stateVariable = (SharedGameObject)FollowTree.GetVariable(_variableName);
        stateVariable.Value = _variable;

    }
    public void SetDeployTreeVariable(GameObject _variable, string _variableName)
    {
        var stateVariable = (SharedGameObject)DeployTree.GetVariable(_variableName);
        stateVariable.Value = _variable;

    }

    public void SetAttackTreeVariable(GameObject _variable, string _variableName)
    {
        var stateVariable = (SharedGameObject)AttackTree.GetVariable(_variableName);
        stateVariable.Value = _variable;

    }

    public virtual void Follow(GameObject _position, GameObject _miradaPosition)
    {
        enableTree("Follow");
        SetTreeVariable(_position, "Position");
        SetTreeVariable(_miradaPosition, "LookAt");
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
        //UI_Attack.attackingUI();
        var stateVariable = (SharedGameObject)AttackTree.GetVariable("Enemy");
        stateVariable.Value = attackPosition;
        enableTree("Attack");


        //set information here
    }


    public virtual IEnumerator gotHit()
    {
        if (hitEffects.Length > 0)
        {
            hitEffects[UnityEngine.Random.Range(0, hitEffects.Length)].Play();
        }
        //Set the main Color of the Material to green
        if (Renderer != null)
        {
            Renderer.material.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(0.1f);
            //Set the main Color of the Material to green
            Renderer.material.SetColor("_Color", Color.white);

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

        var targetVariable = (SharedGameObject)AttackTree.GetVariable("Enemy");
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

    public string getState()
    {
        foreach (BehaviorTree item in AllBehaviour)
        {
            if (item.enabled)
            {
                return item.BehaviorName;
            }
        }
        return null;
    }
    public void ShowAttackUI()
    {
        UI_Attack.PreShow();
    }
    public void StartFillAttackUI(float _time)
    {

        UI_Attack.startFill(_time);
    }




}
