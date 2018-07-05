using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE_RandomHitIndex : StateMachineBehaviour {

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("HitIndex", Random.Range(0, 1));
    }
}
