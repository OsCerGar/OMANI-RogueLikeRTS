using BehaviorDesigner.Runtime;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Robot : NPC
{
    public RobotData data = new RobotData();
    public int level, exp;

    bool link = false;
    Link linky;
    Powers powers = null;
    PowerManager powerManager;
    DissolveEffectController dissolveEffect;
    Army commander;
    public Robot_Energy robot_energy;
    [SerializeField]
    private ParticleSystem SparkEffect;
    [SerializeField]
    private ParticleSystem DematerializeParticleSystem;
    [SerializeField]
    private ParticleSystem MaterializeParticleSystem;
    [HideInInspector]
    public WorkerSM workerSM;
    private float materializeCounter;
    [SerializeField]
    private ParticleSystem DeathExplosion;
    bool materialize = true;
    public Transform ball;
    //reclute pool fix
    public bool recluted;
    float lastTimeTakenDamage;

    public delegate void DieEvent(GameObject _robot);
    public static event DieEvent OnDie;

    //Inputs
    PlayerInputInterface inputController;

    public void Sparks()
    {
        SparkEffect.Play();
    }

    public virtual void Awake()
    {
        workerSM = GetComponentInChildren<WorkerSM>();
        workerSM.transform.parent = null;
        data.robotType = boyType;
    }

    public override void Start()
    {
        base.Start();
        robot_energy = transform.GetComponent<Robot_Energy>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        dissolveEffect = GetComponentInChildren<DissolveEffectController>();
        commander = FindObjectOfType<Army>();
        inputController = FindObjectOfType<PlayerInputInterface>();

        ball = transform.FindDeepChild("StartSphereMesh");

        if (powerPool != maxpowerPool)
        {
            TakeDamage(Mathf.RoundToInt(1f), Color.yellow, transform);
            CoolDown();
        }

        //Read saved Data about the robot

    }
    private void OnEnable()
    {
        state = "Alive";
        if (numberPool != null)
        {
            if (commander.currentFighter != this)
            {
                if (powerPool != maxpowerPool)
                {
                    TakeDamage(Mathf.RoundToInt(maxpowerPool), Color.yellow, transform);
                }
            }
        }


    }

    public override void AttackHit()
    {
        base.AttackHit();
    }

    public override void Update()
    {
        base.Update();
        //DisablesCircle when given an order.
        if (state != "Alive")
        {
            GUI_Script.DisableCircle();
        }
        if (materialize)
        {
            if (materializeCounter > 0)
            {
                materializeCounter -= Time.deltaTime * 2f;
                MK.Toon.MKToonMaterialHelper.SetDissolveAmount(Renderer.material, materializeCounter);
            }
        }
        else
        {

            if (materializeCounter < 1)
            {
                materializeCounter += Time.deltaTime * 2f;
                MK.Toon.MKToonMaterialHelper.SetDissolveAmount(Renderer.material, materializeCounter);
            }
            else
            {
                transform.gameObject.SetActive(false);
            }
        }

        if (commander.currentFighter == this && Time.time - lastTimeTakenDamage > 5f)
        {
            slowPowerpoolHeal(0.01f);
        }
    }

    public virtual void Dematerialize()
    {
        //Dematerializes.

        //Disables everything.

        //hago esto de la posicion porque le quito el parent en el start
        workerSM.transform.position = transform.position;
        workerSM.Dematerialize();

        DematerializeParticleSystem.Play();
        materialize = false;

        Idle();

    }

    public void Materialize(GameObject _ShootingPosition, GameObject _miradaPosition)
    {
        //Dematerializes.
        workerSM.transform.position = transform.position;
        transform.position = _ShootingPosition.transform.position;
        anim.Rebind();
        transform.gameObject.SetActive(true);
        workerSM.Dematerialize();
        MaterializeParticleSystem.Play();


        materialize = true;
        //Disables everything.

        Follow(_ShootingPosition, _miradaPosition);
    }

    public virtual void FighterAttack(GameObject _position)
    {
        SetAttackTreeVariable(_position, "Enemy");
    }

    //Simple way to take damage
    public override void TakeDamage(int damage, Color _damageType, Transform transform)
    {

        StartCoroutine(gotHit());
        numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject, false);

        if (state == "Alive")
        {
            lastTimeTakenDamage = Time.time;
            if (anim != null)
            {
                anim.SetTrigger("Hit");
                inputController.SetVibration(0, 0.5f, 0.25f, false);
                inputController.SetVibration(1, 0.5f, 0.25f, false);

            }
            if (reducePowerNow(damage))
            {
            }
            else
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Disconnected"))
                {
                    //Instead, it should recieve some damage.
                    enableTree("CoolDown");
                    inputController.SetVibration(0, 0.75f, 0.25f, false);
                    inputController.SetVibration(1, 0.75f, 0.25f, false);

                    CoolDown();
                }
                else
                {
                    Die();
                    inputController.SetVibration(0, 1f, 0.25f, false);
                    inputController.SetVibration(1, 1f, 0.25f, false);

                    state = "Dead";
                }
            }
        }
    }

    public override void Die()
    {
        DeathExplosion.transform.parent = null;
        DeathExplosion.transform.position = transform.position + Vector3.up * 2;
        DeathExplosion.Play();
        transform.gameObject.SetActive(false);

        if (OnDie != null)
        {
            OnDie(gameObject);
        }

    }
    public void Deploy(GameObject _flag)
    {
        workerSM.transform.position = _flag.transform.position;
        transform.position = _flag.transform.position;
        anim.Rebind();
        transform.gameObject.SetActive(true);
        workerSM.Dematerialize();
        MaterializeParticleSystem.Play();

        enableTree("Deploy");
        SetDeployTreeVariable(_flag, "Position");


        materialize = true;
        //Disables everything.
    }

    public GameObject getDeployEnemy()
    {
        var targetVariable = (SharedGameObject)DeployTree.GetVariable("Enemy");
        return targetVariable.Value;
    }

    public void AutoReclute()
    {
        commander.Reclute(this);
    }

    public void Fired()
    {
        commander.Remove(this);
    }

    private void CreateLink()
    {
        //CreatesLink
        linky = powerManager.CreateLink(transform, powers).GetComponent<Link>();

        linky.power = powers.gameObject;
        linky.interactible = transform.gameObject;
        link = true;
    }

    private void DestroyLink()
    {
        linky.Failed();
        link = false;
        powerReduced = 0;
    }

    public virtual void CoolDown()
    {

        Nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        enableTree("Cooldown");
        anim.SetTrigger("CoolDown");
        Fired();
    }

    [Serializable]
    public class RobotData
    {
        public int level;
        public int exp;
        public String robotType;
    }
}
