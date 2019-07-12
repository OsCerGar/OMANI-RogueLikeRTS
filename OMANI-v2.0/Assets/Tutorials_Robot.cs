using UnityEngine;

public class Tutorials_Robot : MonoBehaviour
{
    [SerializeField]
    GameObject summonTutorial, spinTutorial, disablePlayer;
    Army commanders;


    [SerializeField]
    GameObject robot;
    bool summoned, tutorialStart;

    public bool version2;


    void Update()
    {

        if (robot.activeInHierarchy) { summoned = true; }

        if (!robot.activeInHierarchy && summoned) { summonTutorial.SetActive(true); //disablePlayer.SetActive(true);
        }
        if (robot.activeInHierarchy && summoned && summonTutorial.activeSelf) { spinTutorial.SetActive(true); //disablePlayer.SetActive(true); 
        }

    }
}
