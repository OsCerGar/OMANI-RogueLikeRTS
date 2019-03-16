using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Robot : NPC
{
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
    bool materialize;
    public Transform ball;

    //reclute pool fix
    public bool recluted;

    public void Sparks()
    {
        SparkEffect.Play();
    }

    public virtual void Awake()
    {
        workerSM = GetComponentInChildren<WorkerSM>();
        workerSM.transform.parent = null;
    }

    public override void Start()
    {
        base.Start();
        robot_energy = transform.GetComponent<Robot_Energy>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        dissolveEffect = GetComponentInChildren<DissolveEffectController>();
        commander = FindObjectOfType<Army>();

        ball = transform.FindDeepChild("StartSphereMesh");

        if (powerPool != maxpowerPool)
        {
            TakeDamage(Mathf.RoundToInt(maxpowerPool), Color.yellow);
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
        transform.gameObject.SetActive(true);
        workerSM.Dematerialize();
        anim.Rebind();
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
    public override void TakeDamage(int damage, Color _damageType)
    {

        StartCoroutine(gotHit());
        numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject);

        if (state == "Alive")
        {
            if (anim != null)
            {
                anim.SetTrigger("Hit");
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
                    CoolDown();
                }
                else
                {
                    Die();
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

}
