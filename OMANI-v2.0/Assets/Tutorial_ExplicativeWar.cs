using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_ExplicativeWar : MonoBehaviour
{

    exPlicativoTreeControler masterWorker;
    LookDirectionsAndOrder lookDirections;

    bool alreadydone = false;
    GameObject warObjective;

    private void Start()
    {
        warObjective = this.transform.GetChild(0).gameObject;
        lookDirections = FindObjectOfType<LookDirectionsAndOrder>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!alreadydone)
            {
                masterWorker = FindObjectOfType<exPlicativoTreeControler>();

                if (lookDirections.playingOnController)
                {
                    masterWorker.ActivateMovementTut(warObjective, "RightStick+L1");
                }
                else
                {
                    masterWorker.ActivateMovementTut(warObjective, "LeftClick");
                }
                alreadydone = true;

            }
        }
    }
}
