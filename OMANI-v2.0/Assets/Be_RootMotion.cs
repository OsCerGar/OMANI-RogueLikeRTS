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
        NAgent = animator.GetComponent<NavMeshAgent>();

        animator.applyRootMotion = true;

        //NAgent.isStopped = true;

        npcScript.RootMotion = true;

        animator.GetComponent<BehaviorTree>().enabled = false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //NAgent.isStopped = false;
        animator.applyRootMotion = false;
        npcScript.RootMotion = false;
        animator.GetComponent<BehaviorTree>().enabled = true;
    }
   

}
