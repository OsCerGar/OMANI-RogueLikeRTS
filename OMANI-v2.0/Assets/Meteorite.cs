using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {
    float x = 0;
    public Vector3 objective;
    Vector3 startPos;
    ParticleSystem explosion;
    public GameObject ExplosionEffect;
    // Use this for initialization
    void Start () {
        explosion = GetComponent<ParticleSystem>();
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        x += Time.deltaTime;
        x = x % 3;
        transform.position = MathParabola.Parabola(startPos,objective,15,x/3);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionEffect,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
