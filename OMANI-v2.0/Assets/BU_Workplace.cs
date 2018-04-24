using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Workplace : MonoBehaviour
{
    BU_PowerPlant powerplant;

    void Awake()
    {
        powerplant = this.transform.parent.GetComponent<BU_PowerPlant>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People") && other.GetComponent<Player>() == null)
        {
            if (powerplant.numberOfWorkers < powerplant.maxnumberOfWorkers)
            {

                if (other.GetComponent<NPC>().AI_GetTarget() == this.transform.parent.gameObject)
                {
                    powerplant.AddWorker(other.gameObject);
                }
            }
        }
    }
}
