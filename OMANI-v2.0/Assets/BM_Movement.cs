using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BM_Movement : MonoBehaviour {
    
        protected Animator animator;
        private float Loop = 0;
        private int footControler = 0;

        public bool ikActive = false;


        public Transform lookObj = null;

        public Transform rightHandPos = null;
        public Transform leftHandPos = null;
        public Transform rightFootPos = null;
        public Transform leftFootPos = null;

    private Vector3 IKrightHandPos;
    private Vector3 IKleftHandPos;
    private Vector3 IKrightFootPos;
    private Vector3 IKleftFootPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        IKrightHandPos = new Vector3(rightHandPos.position.x, rightHandPos.position.y, rightHandPos.position.z);
        IKleftHandPos = new Vector3(leftHandPos.position.x, leftHandPos.position.y, leftHandPos.position.z) ;
        IKrightFootPos = new Vector3(rightFootPos.position.x, rightFootPos.position.y, rightFootPos.position.z);
        IKleftFootPos = new Vector3(leftFootPos.position.x,leftFootPos.position.y, leftFootPos.position.z) ;
    }

    private void Update()
    {
        Loop += Time.deltaTime;
        if (Loop >=0.5)
        {
            switch (footControler)
            {
                case 0:
                    IKrightHandPos = ShootRaycast(rightHandPos);
                    break;
                case 1:
                    IKleftHandPos = ShootRaycast(leftHandPos);
                    break;
                case 2:
                    IKrightFootPos = ShootRaycast(rightFootPos);
                    break;
                case 3:
                    IKleftFootPos = ShootRaycast(leftFootPos);
                    break;

                default:
                    print("Incorrect ");
                    break;
            }
            
            
            
            footControler++;
            if (footControler == 4)
            {
                footControler = 0;
            }
            Loop = 0;
        }

    }

    Vector3 ShootRaycast(Transform tr)
    {
        RaycastHit hit;
        if (Physics.Raycast(tr.position, -Vector3.up, out hit))
        {
            return hit.point;
        }
        return transform.position;
    }

    //a callback for calculating IK
    void OnAnimatorIK()
        {
            if (animator)
            {

                //if the IK is active, set the position and rotation directly to the goal. 
                if (ikActive)
                {

                    // Set the look target position, if one has been assigned
                    if (lookObj != null)
                    {
                        animator.SetLookAtWeight(1);
                        animator.SetLookAtPosition(lookObj.position);
                    }



                // Set the right hand target position and rotation, if one has been assigned
                    if (IKrightHandPos != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightHand, IKrightHandPos);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, leftHandPos.rotation);
                    }
                    if (IKleftHandPos != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand, IKleftHandPos);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandPos.rotation);
                    }
                    if (IKrightFootPos != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightFoot, IKrightFootPos);
                        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootPos.rotation);
                    }
                    if (IKleftFootPos != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftFoot, IKleftFootPos);
                        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootPos.rotation);
                    }
                

                }

                //if the IK is not active, set the position and rotation of the hand and head back to the original position
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }
        }
    
}
