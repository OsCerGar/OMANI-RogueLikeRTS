using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpControl : MonoBehaviour {
    NavMeshAgent agent;
    Animator anim;
    // Update is called once per frame
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update () {
        agent.baseOffset = anim.GetFloat("Height");
	}
}
