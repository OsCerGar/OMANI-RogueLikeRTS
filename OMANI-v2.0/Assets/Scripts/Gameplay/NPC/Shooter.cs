using UnityEngine;

public class Shooter : Robot
{
    [SerializeField] ParticleSystem Shoot;
    [SerializeField] SoundsManager SoundManager;
    bool ik = false;
    int attackcounter = 0;
    GameObject aimAt;
    Vector3 aimAtPos;

    public override void Awake()
    {
        base.Awake();
        boyType = "Shooter";

    }
    public override void Start()
    {
        base.Start();

        //damage = int.Parse(GamemasterController.GameMaster.getCsvValues("Shooter")[2]);
        //damage = Mathf.RoundToInt(damage + (damage * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotBaseDamageLevel", GamemasterController.GameMaster.RobotBaseDamageLevel)[2]) / 100)));

        //maxpowerPool = float.Parse(GamemasterController.GameMaster.getCsvValues("Shooter")[1]);
        //maxpowerPool = Mathf.RoundToInt(maxpowerPool + (maxpowerPool * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotMaxLifeLevel", GamemasterController.GameMaster.RobotMaxLifeLevel)[2]) / 100)));
    }


    public override void Update()
    {
        base.Update();
    }
    public override void AttackHit()
    {
        Shoot.Play();
        SoundManager.AttackHit();
        //reducePowerNow(maxpowerPool);
    }


    public override void FighterAttack(GameObject attackPosition)
    {
        anim.SetBool("Attack", true);
    }
    public override void CoolDown()
    {
        attackcounter = 0;
        reducePowerNow(maxpowerPool);
        enableTree("CoolDown");
        ik = false;
        base.CoolDown();
    }
    public override void Follow(GameObject _position, GameObject _miradaPosition)
    {
        base.Follow(_position, _miradaPosition);
        ik = true;
        aimAt = _miradaPosition;
    }
    void OnAnimatorIK()
    {
        if (ik)
        {
            aimAtPos = new Vector3(aimAt.transform.position.x, aimAt.transform.position.y + 1.5f, aimAt.transform.position.z);
            //righthand
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            anim.SetIKPosition(AvatarIKGoal.RightHand, aimAtPos);
            anim.SetIKRotation(AvatarIKGoal.RightHand, aimAt.transform.rotation);
            //leftHand
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, aimAtPos);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, aimAt.transform.rotation);

        } //if the IK is not active, set the position and rotation of the hand and head back to the original position
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }
}
