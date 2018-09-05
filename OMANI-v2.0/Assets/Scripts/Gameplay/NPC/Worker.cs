using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Robot {
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect;
    void Awake()
    {
        boyType = "Worker";
    }
    public void Trail()
    {
        TrailEffect.Play();
    }
}
