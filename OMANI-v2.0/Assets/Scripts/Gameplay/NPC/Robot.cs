using UnityEngine;
using System.Collections;
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
    private WorkerSM workerSM;
    private float materializeCounter;
    
    public void Sparks()
    {
        SparkEffect.Play();
    }

    
    public override void Start()
    {
        base.Start();
        robot_energy = transform.GetComponent<Robot_Energy>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        dissolveEffect = GetComponentInChildren<DissolveEffectController>();
        commander = FindObjectOfType<Army>();
        workerSM = GetComponentInChildren<WorkerSM>();

        if (powerPool != maxpowerPool)
        {
            powerPool = 1;
            TakeDamage(5, Color.yellow);
        }
    }

    public override void AttackHit()
    {
        base.AttackHit();
        workerSM.DamageDealt();
    }

    public override void Update()
    {
        base.Update();
        //DisablesCircle when given an order.
        if (state != "Alive")
        {
            GUI_Script.DisableCircle();
        }

    }

    public void Dematerialize()
    {
        //Dematerializes.

        //Disables everything.
        StartCoroutine(Dematerialize(0.05f));
        
    }

    public void Materialize(GameObject _ShootingPosition, GameObject _miradaPosition)
    {
        //Dematerializes.

        transform.gameObject.SetActive(true);

        anim.Rebind();

        StopCoroutine(Dematerialize(0.05f));

        StartCoroutine(Materialize(0.05f));
        //Disables everything.
        transform.position = _ShootingPosition.transform.position;

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
        workerSM.DamageRecieved();
        numberPool.NumberSpawn(numbersTransform, damage, _damageType);

        if (state == "Alive")
        {
            if (anim != null)
            {
                anim.SetTrigger("Hit");
            }
            if (powerPool > 0)
            {
                //Instead, it should recieve some damage.
                reducePowerNow(maxpowerPool);
                enableTree("CoolDown");
                CoolDown();
                //Fired();
            }
            else
            {
                Die();
                state = "Dead";
            }
        }
    }

    public override void Die()
    {
        base.Die();

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
        anim.SetTrigger("CoolDown");
        Fired();
    }

    private IEnumerator Dematerialize(float waitTime)
    {
        DematerializeParticleSystem.Play();
        while (materializeCounter < 1)
        {
            materializeCounter += waitTime;
            MK.Toon.MKToonMaterialHelper.SetDissolveAmount(Renderer.material, materializeCounter);
            yield return new WaitForSeconds(waitTime);
        }
        transform.gameObject.SetActive(false);


    }
    private IEnumerator Materialize(float waitTime)
    {

        MaterializeParticleSystem.Play();
        while (materializeCounter > 0)
        {
            materializeCounter -= waitTime;
            MK.Toon.MKToonMaterialHelper.SetDissolveAmount(Renderer.material, materializeCounter);
            yield return new WaitForSeconds(waitTime);
        }


    }
}
