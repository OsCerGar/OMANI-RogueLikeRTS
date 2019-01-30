using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class RangedAttack : MonoBehaviour {
    
    [SerializeField] bool Knockback;
    [SerializeField] int Damage = 1;
    [SerializeField] NPC thisNpcScript;
    [SerializeField] LayerMask LayerMasktoAttack;




    void OnParticleCollision(GameObject other)
    {
        if (IsInLayerMask(other.gameObject, LayerMasktoAttack))
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            if (Damage == 0)
            {
                Damage = thisNpcScript.Damage;
            }

            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(Damage, true, 5, transform.parent.transform);

            }
            else
            {
                EnemyNPC.TakeDamage(Damage);
            }
            

        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // Convert the object's layer to a bitfield for comparison
        int objLayerMask = (1 << obj.layer);
        if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
            return true;
        else
            return false;
    }
}
