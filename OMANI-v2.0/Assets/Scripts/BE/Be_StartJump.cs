using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class Be_StartJump : StateMachineBehaviour {
    public GameObject efect;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //start the jumping in tree behaviour (seek)
        var targetVariable = (SharedBool)animator.gameObject.GetComponent<BehaviorTree>().GetVariable("Jump");
        targetVariable.Value = true;

        var position = (SharedVector3)animator.gameObject.GetComponent<BehaviorTree>().GetVariable("JumpObjective");
        position.Value = (animator.transform.Find("JumpPosition").transform.position);

      
        

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
