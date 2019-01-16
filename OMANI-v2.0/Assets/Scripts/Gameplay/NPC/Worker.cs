using UnityEngine;

public class Worker : Robot
{
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect;

    [SerializeField]
    private GameObject RollHillBox;

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

        enableTree("Attack");
        Fired();
    }

    void Awake()
    {
        boyType = "Worker";
    }
    public void Trail()
    {
        TrailEffect.Play();
    }
    public void FlipSound()
    {
        WorkerSM wsm = (WorkerSM)SM;
        wsm.Flip();
    }
    public void RollAttack()
    {
        Trail();
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
