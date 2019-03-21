using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinalutorialBoss : MonoBehaviour
{
   [SerializeField] TIMELINE_INTERFACE TInterface;
   [SerializeField] Animator CityGate;
    [SerializeField] GameObject MusicManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CityGate.SetTrigger("Close");
            TInterface.TPlay();
            MusicManager.SetActive(false);
        }
    }
}
