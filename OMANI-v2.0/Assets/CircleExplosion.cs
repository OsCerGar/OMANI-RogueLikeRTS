using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleExplosion : MonoBehaviour {
    float size = 0.1f;
    ParticleSystem ps;
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        size += Time.deltaTime;
        transform.localScale = new Vector3(size, size, size);
        if (size > 3)
        {
            ps.Stop();
        }
    }
}
