using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Be_RootMotion : StateMachineBehaviour {
    NPC npcScript;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = true;
        npcScript = animator.GetComponent<NPC>();
        npcScript.RootMotion = true;
        animator.GetComponent<BehaviorTree>().enabled = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = false;
        npcScript.RootMotion = false;
        animator.GetComponent<BehaviorTree>().enabled = true;
    }
   

}
