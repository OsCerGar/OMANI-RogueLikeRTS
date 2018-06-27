using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Door : MonoBehaviour
{

    exPlicativoTreeControler masterWorker;
    bool alreadydone = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!alreadydone)
            {
                masterWorker = FindObjectOfType<exPlicativoTreeControler>();

                masterWorker.ActivateFollow();
                alreadydone = true;
            }
        }
    }
}
