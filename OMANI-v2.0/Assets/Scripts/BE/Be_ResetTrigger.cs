using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Be_ResetTrigger : StateMachineBehaviour {
    [SerializeField] string triggername;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.ResetTrigger(triggername);
    }

}
