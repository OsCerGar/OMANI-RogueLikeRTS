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
        Attackzone.SetActive(true);
        //RollDisableCheck();
        TrailEffect.Stop();
    }
    public override void FighterAttack(GameObject _position)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("react"))
        {
            if (anim.GetBool("Roll"))
            {
                RollAttackFinished();
            }
            else
            {
                base.FighterAttack(_position);
                StartRollAttack();
                enableTree("Attack");
            }
        }
    }
    public void DisableStearing()
    {
        TPC.Rotate = false;
    }
    public void EnableStearing()
    {
        TPC.Rotate = true;
    }
    public override void Awake()
    {
        base.Awake();
        boyType = "Worker";
        LookDAO = FindObjectOfType<LookDirectionsAndOrder>();
    }
    public override void Start()
    {
        base.Start();
        //damage
        damage = int.Parse(GamemasterController.GameMaster.getCsvValues("Worker")[2]);
        damage = Mathf.RoundToInt(damage + (damage * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotBaseDamageLevel", GamemasterController.GameMaster.RobotBaseDamageLevel)[2]) / 100)));
        //life
        maxpowerPool = float.Parse(GamemasterController.GameMaster.getCsvValues("Worker")[1]);
        maxpowerPool = Mathf.RoundToInt(maxpowerPool + (maxpowerPool * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotMaxLifeLevel", GamemasterController.GameMaster.RobotMaxLifeLevel)[2]) / 100)));
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
        enableTree("Attack");
        anim.SetBool("Roll", true);
        Trail();
        workerSM.Flip();
        RollHillBox.SetActive(true);
    }
    public void RollAttackFinished()
    {
        enableTree("Follow");
        anim.SetBool("Roll", false);
        RollHillBox.SetActive(false);
    }
    public void RollCollision()
    {
        anim.SetTrigger("AttackCollision");
    }

    public override void CoolDown()
    {
        RollDisableCheck();
        base.CoolDown();
    }

    public override void Die()
    {
        RollDisableCheck();
        base.Die();
    }
    public override void Dematerialize()
    {
        RollDisableCheck();
        base.Dematerialize();
    }
    private void RollDisableCheck()
    {
        anim.SetBool("Roll", false);
        RollHillBox.SetActive(false);
        LookDAO.AlternativeCenter(null);
    }

}
