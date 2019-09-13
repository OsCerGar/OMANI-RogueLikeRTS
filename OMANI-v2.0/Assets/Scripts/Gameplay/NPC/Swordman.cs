using UnityEngine;

public class Swordman : Robot
{
    [SerializeField] GameObject Attack1Zone;
    [SerializeField] GameObject Attack2Zone;
    [SerializeField] int numberOfAttacks = 4;
    int attackcounter = 0;
    public override void Awake()
    {
        base.Awake();
        boyType = "Swordsman";



    }
    public override void Start()
    {
        base.Start();

        //damage
        //damage = int.Parse(GamemasterController.GameMaster.getCsvValues("Warrior")[2]);
        //damage = Mathf.RoundToInt(damage + (damage * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotBaseDamageLevel", GamemasterController.GameMaster.RobotBaseDamageLevel)[2]) / 100)));

        //MaxPowerPoolCalc
        //maxpowerPool = float.Parse(GamemasterController.GameMaster.getCsvValues("Warrior")[1]);
        //maxpowerPool = Mathf.RoundToInt(maxpowerPool + (maxpowerPool * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotMaxLifeLevel", GamemasterController.GameMaster.RobotMaxLifeLevel)[2]) / 100)));

    }
    public override void Update()
    {
        base.Update();
    }
    public override void AttackHit()
    {
        base.AttackHit();
    }
    public void Attack1()
    {
        Attack1Zone.SetActive(true);
        //attackcounter++;
    }
    public void Attack2()
    {
        Attack2Zone.SetActive(true);
    }

    public override void FighterAttack(GameObject attackPosition)
    {
        if (attackcounter < numberOfAttacks)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("FinalAttack", true);
        }
    }
    public override void CoolDown()
    {
        attackcounter = 0;
        reducePowerNow(maxpowerPool);
        enableTree("CoolDown");
        base.CoolDown();
    }
}
