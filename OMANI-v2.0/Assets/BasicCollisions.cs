using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCollisions : MonoBehaviour
{

    [SerializeField] ParticleSystem PS;
    private void OnTriggerEnter(Collider other)
    {
        Enemy NPC;
        Interactible interactible;
        if (other.CompareTag("Enemy"))
        {
            NPC = other.GetComponent<Enemy>();
            PS.transform.position = other.transform.position;
            PS.Play();
            NPC.TakeDamage(5, true, 0.2f, this.transform);
        }
        else if (other.CompareTag("Building"))
        {
            interactible = other.GetComponent<Interactible>();
            interactible.Action();
        }
    }

}
