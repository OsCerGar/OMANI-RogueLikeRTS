using UnityEngine;

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

    public void Sparks()
    {
        SparkEffect.Play();
    }

    public void StartResurrection()
    {
        anim.SetTrigger("GetUp");
        dissolveEffect.StartRevert();
    }
    public override void Start()
    {
        base.Start();
        robot_energy = transform.GetComponent<Robot_Energy>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        dissolveEffect = GetComponentInChildren<DissolveEffectController>();
        commander = FindObjectOfType<Army>();
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

    //Simple way to take damage
    public override void TakeDamage(int damage)
    {

        StartCoroutine(gotHit());

        if (state == "Alive")
        {
            if (anim != null)
            {
                anim.SetTrigger("Hit");
            }
            if (powerPool > 0)
            {
                reducePowerNow(maxpowerPool);
                enableTree("CoolDown");
                Fired();
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
        //dissolveEffect.StartDissolve();
    }
    public void Resurrect()
    {

        Nav.updatePosition = true;
        Nav.updateRotation = true;
        life = startLife;
        //this.gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.layer = peopl;
        state = "Alive";
        //cambiar tag y layer
    }

    public void AutoReclute()
    {
        commander.Reclute(this);
    }

    public void Fired()
    {
        commander.RemoveFromList(this);
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


}
