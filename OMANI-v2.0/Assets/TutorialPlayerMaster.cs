using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerMaster : MonoBehaviour
{
    [SerializeField] Transform tutorialPoint1, tutorialPoint2;
    // Start is called before the first frame update
    void Start()
    {
        if (TutorialGameMaster.tutorialGameMaster.PointReached > 0) { this.gameObject.transform.position = tutorialPoint1.transform.position; }
        if (TutorialGameMaster.tutorialGameMaster.PointReached > 1) { this.gameObject.transform.position = tutorialPoint2.transform.position; }
    }
}
