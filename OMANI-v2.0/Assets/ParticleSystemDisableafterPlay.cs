using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDisableafterPlay : MonoBehaviour
{
    public ParticleSystem Particles;

    void Awake()
    {
        if (Particles == null)
            Particles = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        if (Particles == null)
        {
            Debug.LogError("ParticleSystemDisable " + gameObject.name + " could not find any particle systems!");
        }
    }

    void Update()
    {
        if (!Particles.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}
