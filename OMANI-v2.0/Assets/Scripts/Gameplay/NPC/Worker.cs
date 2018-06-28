using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : Robot {
    public GameObject Scrap;
    private ParticleSystem explosion;
    void Awake()
    {
        boyType = "Worker";
    }
    public void Explode()
    {
        explosion.Play();
        Instantiate(Scrap,transform.position,transform.rotation);
        Instantiate(Scrap, transform.position, transform.rotation);
        Instantiate(Scrap, transform.position, transform.rotation);
        transform.gameObject.SetActive(false);
    }
}
