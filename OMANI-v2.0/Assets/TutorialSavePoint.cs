using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSavePoint : MonoBehaviour
{
    [SerializeField] int savePoint;
    private void OnTriggerEnter(Collider other)
    {
        TutorialGameMaster.tutorialGameMaster.PointReached = savePoint;
    }
}
