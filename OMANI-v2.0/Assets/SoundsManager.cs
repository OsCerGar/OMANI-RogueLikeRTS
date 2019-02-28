using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {
    public AudioSource AttackAudioSource;
     public AudioSource AS;
    [SerializeField] AudioClip[] HitClips;
    [SerializeField] AudioClip[] MissClips;
    [SerializeField] AudioClip[] Steps;
    
    public void AttackHit()
    {
        AttackAudioSource.clip = HitClips[Random.Range(0, HitClips.Length-1)];
        AttackAudioSource.Play();
    }
    public void AttackMiss()
    {
        AttackAudioSource.clip = MissClips[Random.Range(0, HitClips.Length - 1)];
        AttackAudioSource.Play();

    }
    public void Step()
    {
        AS.clip = Steps[Random.Range(0, HitClips.Length - 1)];
        AS.Play();
    }
}
