using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : NPC {
    public GameObject Scrap;
    private ParticleSystem explosion;
    void Awake()
    {
        boyType = "Swordsman";
    }
    public void Explode()
    {
        explosion.Play();
        Instantiate(Scrap,transform.position,transform.rotation);
        Instantiate(Scrap, transform.position, transform.rotation);
        Instantiate(Scrap, transform.position, transform.rotation);
        Destroy(transform.gameObject);
    }
}
