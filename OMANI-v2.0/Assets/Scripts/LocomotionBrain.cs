using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionBrain : MonoBehaviour
{
    private float Loop = 0, loopCycle = 50, armSpeed = 3f;
    private int footControler = 0;

    public bool ikActive = false;


    public Transform lookObj = null;

    public Transform rightHandPos = null;
    public Transform leftHandPos = null;
    public Transform rightFootPos = null;
    public Transform leftFootPos = null;

    public GameObject ArmPositions;

    private Vector3 IKrightHandPos;
    private Vector3 IKleftHandPos;
    private Vector3 IKrightFootPos;
    private Vector3 IKleftFootPos;

    private ArmBrain RightHandBrain;
    private ArmBrain LeftHandBrain;
    private ArmBrain RightFootBrain;
    private ArmBrain LeftFootBrain;
    CharacterController playerRB;

    int layer_mask;

    void Start()
    {
        playerRB = transform.parent.GetComponentInChildren<CharacterController>();
        layer_mask = LayerMask.GetMask("Terrain");
        RightHandBrain = rightHandPos.GetComponent<ArmBrain>();
        LeftHandBrain = leftHandPos.GetComponent<ArmBrain>();
        RightFootBrain = rightFootPos.GetComponent<ArmBrain>();
        LeftFootBrain = leftFootPos.GetComponent<ArmBrain>();
        IKrightHandPos = new Vector3(rightHandPos.position.x, rightHandPos.position.y, rightHandPos.position.z);
        IKleftHandPos = new Vector3(leftHandPos.position.x, leftHandPos.position.y, leftHandPos.position.z);
        IKrightFootPos = new Vector3(rightFootPos.position.x, rightFootPos.position.y, rightFootPos.position.z);
        IKleftFootPos = new Vector3(leftFootPos.position.x, leftFootPos.position.y, leftFootPos.position.z);
    }

    private void Update()
    {
        Loop += Time.unscaledDeltaTime;
        if (Loop >= 0.2 - (playerRB.velocity.magnitude / loopCycle))
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
        if (ikActive)
        {
            // Set the right hand target position and rotation, if one has been assigned
            if (IKrightHandPos != null)
            {
                RightHandBrain.SetTargetPos(IKrightHandPos);
            }
            if (IKleftHandPos != null)
            {
                LeftHandBrain.SetTargetPos(IKleftHandPos);
            }
            if (IKrightFootPos != null)
            {
                RightFootBrain.SetTargetPos(IKrightFootPos);
            }
            if (IKleftFootPos != null)
            {

                LeftFootBrain.SetTargetPos(IKleftFootPos);
            }


        }

        //if the IK is not active, set the position and rotation of the hand and head back to the original position
        else
        {
            //TODO
        }

        ArmPositions.transform.position = playerRB.transform.position + playerRB.velocity / armSpeed; //adjust to rB velocity
        
        var MaxDistance = 5f;
        if (Vector3.Distance(IKrightHandPos, ShootRaycast(rightHandPos)) > MaxDistance)
        {
            IKrightHandPos = ShootRaycast(rightHandPos);
            RightHandBrain.SetTargetPos(IKrightHandPos);
        }
        if (Vector3.Distance(IKleftHandPos, ShootRaycast(leftHandPos)) > MaxDistance)
        {
            IKleftHandPos = ShootRaycast(leftHandPos);
            LeftHandBrain.SetTargetPos(IKleftHandPos);
        }
        if (Vector3.Distance(IKrightFootPos, ShootRaycast(rightFootPos)) > MaxDistance)
        {
            IKrightFootPos = ShootRaycast(rightFootPos);
            RightFootBrain.SetTargetPos(IKrightFootPos);
        }
        if (Vector3.Distance(IKleftFootPos, ShootRaycast(leftFootPos)) > MaxDistance)
        {
            IKleftFootPos = ShootRaycast(leftFootPos);
            LeftFootBrain.SetTargetPos(IKleftFootPos);
        }
        



    }

    Vector3 ShootRaycast(Transform tr)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(tr.position.x, tr.position.y + 10, tr.position.z), -Vector3.up, out hit, layer_mask))
        {
            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }else
        {
            return tr.position;
        }
    }

    public void SlowMotionValues()
    {
        loopCycle = loopCycle * 5;
        armSpeed = armSpeed * 5;
    }
    public void normalValues()
    {
        loopCycle = loopCycle / 5;
        armSpeed = armSpeed / 5;
    }

}
