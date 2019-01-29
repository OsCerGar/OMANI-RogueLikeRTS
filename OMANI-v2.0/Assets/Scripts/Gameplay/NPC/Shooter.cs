using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Robot
{
    [SerializeField] ParticleSystem Shoot;
    bool ik = false;
    int attackcounter = 0;
    GameObject aimAt;
    void Awake()
    {
        boyType = "Shooter";
    }
    public override void Update()
    {
        base.Update();
    }
    public override void AttackHit()
    {

        Shoot.Play();
        reducePowerNow(maxpowerPool);
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
            //righthand
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    anim.SetIKPosition(AvatarIKGoal.RightHand, aimAt.transform.position);
                    anim.SetIKRotation(AvatarIKGoal.RightHand, aimAt.transform.rotation);
            //leftHand
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, aimAt.transform.position);
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
