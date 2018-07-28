using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [HideInInspector]
    public CharacterMovement player;
    [HideInInspector]
    public Powers powers;
    [HideInInspector]
    public LocomotionBrain locomotionBrain;

    public virtual void Awake()
    {
        locomotionBrain = FindObjectOfType<LocomotionBrain>();
        player = GetComponent<CharacterMovement>();
        powers = FindObjectOfType<Powers>();
    }

    public virtual void Update()
    {
    }

    public virtual void CastPower()
    {
        // do the power
    }

}
