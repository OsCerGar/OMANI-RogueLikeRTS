using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject thingToDisable;
    bool enabledd;


    private void OnTriggerEnter(Collider other)
    {
        if (!enabledd)
        {
            if (other.CompareTag("Player"))
            {
                thingToDisable.SetActive(false);
                enabledd = true;
            }
        }
    }

}
