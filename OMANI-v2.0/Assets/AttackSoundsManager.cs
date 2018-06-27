using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSoundsManager : MonoBehaviour {
    AudioSource AS;
    [SerializeField] AudioClip[] HitClips;
    [SerializeField] AudioClip[] MissClips;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    public void AttackHit()
    {
        AS.clip = HitClips[Random.Range(0, HitClips.Length-1)];
        AS.Play();
    }
    public void AttackMiss()
    {
        AS.clip = MissClips[Random.Range(0, HitClips.Length - 1)];
        AS.Play();

    }
}
