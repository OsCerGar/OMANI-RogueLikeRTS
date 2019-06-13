using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour {
   
    float damageTickcd = 0,cd = 1;
    [SerializeField]
    int Damage;

    private void Update()
    {

        damageTickcd += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "People")
        {
            if (damageTickcd > cd)
            {
                var npc = other.transform.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.TakeDamage(1, Color.yellow, transform);
                }
                damageTickcd = 0;
            }
           
        }
    }
}
