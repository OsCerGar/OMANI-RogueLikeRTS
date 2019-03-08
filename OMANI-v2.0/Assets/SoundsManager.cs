using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {
    public AudioSource AttackAudioSource;
     public AudioSource StepAudioSource;
    [SerializeField] AudioClip[] HitClips;
    [SerializeField] AudioClip[] MissClips;
    [SerializeField] AudioClip[] Steps;

    




    public void AttackHit()
    {
         if (HitClips.Length > 0)
        {
            AttackAudioSource.clip = HitClips[Random.Range(0, HitClips.Length)];
            AttackAudioSource.Play();
        }
    }
    public void AttackMiss()
    {

        if (MissClips.Length > 0)
        {
            AttackAudioSource.clip = MissClips[Random.Range(0, MissClips.Length)];
            AttackAudioSource.Play();
        }

    }
    public void Step()
    {
        if (Steps.Length > 0)
        {
            StepAudioSource.clip = Steps[Random.Range(0, Steps.Length)];
            StepAudioSource.Play();
        }
    }

}
