using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Workplace : MonoBehaviour
{
    BU building;

    void Awake()
    {
        building = this.transform.parent.GetComponent<BU>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People") && other.GetComponent<Player>() == null)
        {
            if (building.numberOfWorkers < building.maxnumberOfWorkers)
            {

                if (other.GetComponent<NPC>().AI_GetEnemy() == building.direction)
                {
                    building.AddWorker(other.gameObject);
                }
            }
        }
    }
}
