using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBrain : MonoBehaviour {

    [SerializeField] Transform ArmTarget;
    public Vector3 TargetPos;
    private Vector3 startPos;
    private bool moving;
    float x = 0;
    private void Start()
    {
        TargetPos = ArmTarget.position;
        ArmTarget.parent = null;
        moving = false;
    }
    private void Update()
    {
        if (moving)
        {
            x += (Time.unscaledDeltaTime * 7f);
            ArmTarget.position = MathParabola.Parabola(startPos, TargetPos, 0.5f,  x);
            if (x>0.95)
            {
                x = 0;
                moving = false;

            }
        }
    }
    public void SetTargetPos(Vector3 _targetPos)
    {
        if (Vector3.Distance(_targetPos,TargetPos) > 1)
        {
            startPos = new Vector3(ArmTarget.position.x, ArmTarget.position.y, ArmTarget.position.z);
            TargetPos = _targetPos;
            moving = true;
            x = 0;
        }
    }
}
