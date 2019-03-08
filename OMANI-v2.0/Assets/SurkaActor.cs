using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurkaActor : MonoBehaviour
{
    [HideInInspector] public SoundsManager SM;
    private void Start()
    {

        SM = GetComponentInChildren<SoundsManager>();
    }
    public void StepSound()
    {
        SM.Step();
    }
}
