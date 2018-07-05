using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedPower : MonoBehaviour {
    [SerializeField]ParticleSystem PS;
    private void OnTriggerEnter(Collider other)
    {
            Enemy NPC;
            if (NPC = other.GetComponent<Enemy>())
            {
                PS.transform.position = other.transform.position;
                PS.Play();
                NPC.TakeDamage(25);
            }
    }
}
