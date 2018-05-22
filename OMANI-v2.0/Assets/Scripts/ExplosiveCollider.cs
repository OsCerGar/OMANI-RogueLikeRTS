using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCollider : MonoBehaviour {
    ParticleSystem Explosion;
    private void Start()
    {
        Explosion = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        NPC npc;
        if (npc = collision.transform.GetComponent<NPC>())
        {
            npc.Life -= 50;
            Explosion.transform.parent = null;
            Explosion.Play();
            Destroy(transform.gameObject);
        }
    }
}
