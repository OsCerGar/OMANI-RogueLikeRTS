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
    public LaserColision laserColision;
    private Vector3 mouseWorldPosition;
    private Animator anim;
    private float contador = 0;
    ConLaser CLaser;
    LookDirectionsAndOrder lookdir;

    void Start () {
        anim = GetComponent<Animator>();
        CLaser = GetComponentInChildren<ConLaser>();
        lookdir = FindObjectOfType<LookDirectionsAndOrder>();
        laserColision = FindObjectOfType<LaserColision>();
    }

    private void Update()
    {

        if (contador > 0)
        {
            contador -= Time.deltaTime;
        } else
        {
            anim.SetBool("Fire", false);
            laserColision.laserEnabled = false;
        }

    }
    private void LateUpdate()
    {
        Quaternion toRotation = Quaternion.LookRotation(lookdir.miradaposition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        
    }

    public  void EmitOffensiveLaser()
    {
        StartEffects();
        contador = 1;
        anim.SetBool("Fire", true);
        Instantiate(laserShotPrefab, laserShotPosition.position, transform.rotation);
    }

    public  void StartEffects()
    {
        startWavePS.Emit(1);
        startParticles.Emit(startParticlesCount);
    }

    public  void EmitLaser()
    {
        CLaser.SetGlobalProgress();
        contador = 1;
        anim.SetBool("Fire", true);
        laserColision.laserEnabled = true;
    }

}
