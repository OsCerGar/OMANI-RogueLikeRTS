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
        base.FighterAttack(_position);
        RollAttack();
        enableTree("Attack");
        Fired();
    }

    void Awake()
    {
        boyType = "Worker";
        wsm = GetComponentInChildren<WorkerSM>();
    }
    public void Trail()
    {
        TrailEffect.Play();
    }
    public void FlipSound()
    {
        wsm.Flip();
    }
    public void RollAttack()
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
    }


}
