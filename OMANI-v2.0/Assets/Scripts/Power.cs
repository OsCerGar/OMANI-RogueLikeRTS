using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    [SerializeField] public ParticleSystem mainParticleSystem;
    public CharacterMovement player;
    public Powers powers;
    public LocomotionBrain locomotionBrain;

    public virtual void Awake()
    {
        locomotionBrain = FindObjectOfType<LocomotionBrain>();
        player = GetComponent<CharacterMovement>();
        powers = FindObjectOfType<Powers>();
    }

    public virtual void Update()
    {
        if (mainParticleSystem.IsAlive() == false)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    public virtual void CastPower()
    {
        // do the power
    }

}
