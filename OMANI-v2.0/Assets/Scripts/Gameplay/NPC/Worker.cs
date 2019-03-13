using UnityEngine;

public class Worker : Robot
{
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect;

    [SerializeField]
    private GameObject RollHillBox;

    WorkerSM wsm;
    public bool animationRollAttack;

    public override void AttackHit()
    {
        base.AttackHit();
        RollAttackFinished();
        TrailEffect.Stop();
    }
    public override void FighterAttack(GameObject _position)
    {
        if (anim.GetBool("Roll"))
        {
            RollAttackFinished();
            anim.SetBool("Roll", false);

        }
        else
        {
            base.FighterAttack(_position);
            StartRollAttack();
            //enableTree("Attack");
            anim.SetBool("Roll",true);
        }
    }

    void Awake()
    {
        boyType = "Worker";
        wsm = GetComponentInChildren<WorkerSM>();
    }
    public void Trail()
    {
        TrailEffect.gameObject.SetActive(true);
        TrailEffect.Play();
    }
    public void FlipSound()
    {
        wsm.Flip();
    }
    public void StartRollAttack()
    {
        Trail();
        wsm.Flip();
        RollHillBox.SetActive(true);
    }
    public void RollAttackFinished()
    {
        RollHillBox.SetActive(false);
    }
    public void RollCollision()
    {
        anim.SetTrigger("AttackCollision");
        Fired();
    }


}
