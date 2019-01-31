using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanAttack : MonoBehaviour {
    [SerializeField] Transform mouth;
    int damage = 25;
    [SerializeField] ParticleSystem ps;
	void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(mouth.position, transform.forward,out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<NPC>().TakeDamage(damage, Color.white); ;
            }

            ps.transform.position = hit.point;
            ps.Play();
        }
           
    }
}
