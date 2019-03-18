using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerRoll : MonoBehaviour {
    [SerializeField] LayerMask LayerMasktoAttack;
    [SerializeField] Worker thisNpcScript;

   
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {

        if (IsInLayerMask(other.gameObject, LayerMasktoAttack))
        {
            thisNpcScript.RollCollision();
            thisNpcScript.AttackHit();

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
