using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Laser : MonoBehaviour {

    public Transform laserShotPosition;
    public float speed = 1f;
    public ParticleSystem startWavePS;
    public ParticleSystem startParticles;
    public int startParticlesCount = 100;
    public GameObject laserShotPrefab;

    private Vector3 mouseWorldPosition;
    private Animator anim;
    private float contador = 0;
    ConLaser CLaser;

    void Start () {
        anim = GetComponent<Animator>();
        CLaser = GetComponentInChildren<ConLaser>();
    }

    private void Update()
    {

        if (contador > 0)
        {
            contador -= Time.deltaTime;
        } else
        {
            anim.SetBool("Fire", false);
        }
        
    }

    public  void EmitOffensiveLaser(Vector3 mouse)
    {
        transform.LookAt(mouse);
        contador = 1;
        anim.SetBool("Fire", true);
        startWavePS.Emit(1);
        startParticles.Emit(startParticlesCount);
        Instantiate(laserShotPrefab, laserShotPosition.position, transform.rotation);
    }

    public  void EmitLaser(Vector3 mouse)
    {

        transform.LookAt(mouse);
        CLaser.SetGlobalProgress();
        contador = 1;
        anim.SetBool("Fire", true);
        startWavePS.Emit(1);
        startParticles.Emit(startParticlesCount);
    }

}
