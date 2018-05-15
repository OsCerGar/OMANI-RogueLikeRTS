using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {
    float x = 0;
    public Vector3 objective;
    Vector3 startPos;
    Vector3 direction;
    ParticleSystem explosion;
    public GameObject ExplosionEffect;
    // Use this for initialization
    void Start () {
        explosion = GetComponentInChildren<ParticleSystem>();
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
         direction = objective - startPos;
	}
	
	// Update is called once per frame
	void Update () {
        x += Time.deltaTime;
        x = x % 2;
        transform.position = MathParabola.Parabola(startPos,objective,15,x/2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        explosion.transform.parent = null;
        explosion.Play();
        var exp =Instantiate(ExplosionEffect,new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        Rigidbody rb;
        if (rb = exp.GetComponent<Rigidbody>())
        {
            rb.AddForce(direction * 50);
        }
        Destroy(this.gameObject);
    }
}
