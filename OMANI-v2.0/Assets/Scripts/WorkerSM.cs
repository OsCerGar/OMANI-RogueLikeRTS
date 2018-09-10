using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSM : SoundsManager
{
    [SerializeField] AudioClip FlipClip;
    public void Flip()
    {
        AS.clip = FlipClip;
        AS.Play();
    }

}
