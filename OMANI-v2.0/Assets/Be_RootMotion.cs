using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Be_RootMotion : StateMachineBehaviour {
    NPC npcScript;
    NavMeshAgent NAgent;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        npcScript = animator.GetComponent<NPC>();

        animator.applyRootMotion = true;
        

        npcScript.RootMotion = true;

        animator.GetComponent<BehaviorTree>().DisableBehavior();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = false;
        npcScript.RootMotion = false;
        animator.GetComponent<BehaviorTree>().EnableBehavior();
    }
   

}
