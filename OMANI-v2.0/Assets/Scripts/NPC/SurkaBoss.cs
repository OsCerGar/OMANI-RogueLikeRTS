using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Playables;

public class SurkaBoss : Enemy
{
    [SerializeField] ParticleSystem AttackTrail;
    [SerializeField] ParticleSystem Slash;
    [SerializeField] ParticleSystem Shoot;
    [SerializeField] ParticleSystem ShootPuke;
    [SerializeField] string TreeToEnable;
    [SerializeField] PlayableDirector Director;

    bool fase1, fase2, fase3;

    [SerializeField]
    RPGTalk dieDialog;

    public void SetMaster(GameObject master)
    {
        var thisTarget = (SharedGameObject)transform.gameObject.GetComponent<BehaviorTree>().GetVariable("Master");
        thisTarget.Value = master;

    }
    public void StartAttackTrail()
    {
        AttackTrail.Play();
    }

    public override void AttackHit()
    {
        Attackzone.SetActive(true);
        Slash.Play();
    }
    public void Spit()
    {
        Shoot.Emit(2);
        SM.AttackHit();
    }
    public void Puke()
    {
        ShootPuke.Play();
    }

    public override void Start()
    {
        base.Start();
        enableTree(TreeToEnable);

    }
    public override void Update()
    {
        base.Update();
        if (!fase1)
        {
            if (life > startLife / 3 * 2)
            {
                enableTree("Idle");
                fase1 = true;
            }
        }
        else
        if (!fase2)
        {
            if (life < startLife / 3 * 2)
            {
                enableTree("IdleFase2");
                fase2 = true;
            }
        }
        if (!fase3)
        {
            if (life < startLife / 3)
            {
                enableTree("IdleFase3");
                fase3 = true;
            }
        }



    }
    public override void Die()
    {
        base.Die();

        dieDialog.variables[0].variableValue = GamemasterController.GameMaster.Money.ToString();
        Director.Play();
    }


}
