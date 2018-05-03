using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour {
   [SerializeField]
    string TagToDamage;

    [SerializeField]
    int Damage;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == (TagToDamage))
        {
            var npc = other.transform.GetComponent<NPC>();
            if (npc != null)
            {
                npc.Life -= Damage;
            }
        }
    }
}
