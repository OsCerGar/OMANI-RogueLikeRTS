using UnityEngine;

public class Worker : Robot
{
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect;

    [SerializeField]
    private GameObject RollHillBox;
    LookDirectionsAndOrder LookDAO;
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
            enableTree("Follow");
            RollAttackFinished();
            anim.SetBool("Roll", false);

        }
        else
        {
            base.FighterAttack(_position);
            enableTree("Attack");
            StartRollAttack();
            //enableTree("Attack");
            anim.SetBool("Roll",true);
        }
    }

    public override void Awake()
    {
        base.Awake();
        boyType = "Worker";
        LookDAO = FindObjectOfType<LookDirectionsAndOrder>();
    }
    public void Trail()
    {
        TrailEffect.gameObject.SetActive(true);
        TrailEffect.Play();
    }
    public void FlipSound()
    {
        workerSM.Flip();
    }
    public void StartRollAttack()
    {
        LookDAO.AlternativeCenter(transform);
        Trail();
        workerSM.Flip();
        RollHillBox.SetActive(true);
    }
    public void RollAttackFinished()
    {
        RollHillBox.SetActive(false);
        LookDAO.AlternativeCenter(null);
    }
    public void RollCollision()
    {
        anim.SetTrigger("AttackCollision");
        Fired();
    }


}
