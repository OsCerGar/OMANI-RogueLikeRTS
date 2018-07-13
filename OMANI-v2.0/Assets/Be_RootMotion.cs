using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

public class Be_RootMotion : StateMachineBehaviour {
    NPC npcScript;
    NavMeshAgent NAgent;
    public bool Restore = true;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NAgent = animator.GetComponent<NavMeshAgent>();
        npcScript = animator.GetComponent<NPC>();
        
       // NAgent.updatePosition = false;
        npcScript.RootMotion = true;

        animator.GetComponent<BehaviorTree>().DisableBehavior();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Restore)
        {

            //NAgent.updatePosition = true;
            npcScript.RootMotion = false;
            animator.GetComponent<BehaviorTree>().EnableBehavior();
        }
    }
   

}
