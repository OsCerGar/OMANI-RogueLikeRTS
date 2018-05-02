using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollider : MonoBehaviour {
   [SerializeField]
    string TagToDamage;

    [SerializeField]
    int Damage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag ==(TagToDamage))
        {
            var npc = collision.transform.GetComponent<NPC>();
            if (npc != null)
            {
                npc.Life -= Damage;
            }
        }
    }
}
