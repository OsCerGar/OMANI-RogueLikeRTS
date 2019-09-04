using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTimelineMaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TutorialGameMaster.tutorialGameMaster.PointReached> 0) { this.gameObject.SetActive(false); }

    }

}
