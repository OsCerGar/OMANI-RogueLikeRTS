using UnityEngine;

public class Tutorials_Robot : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialRight, tutorialLeft;
    Army commanders;


    [SerializeField]
    GameObject robot;
    bool summoned, tutorialStart;

    public bool version2;
    // Start is called before the first frame update
    void Awake()
    {
        if (tutorialRight == null)
        {
            tutorialRight = transform.FindDeepChild("Tutorial_Right").gameObject;
        }

        if (tutorialLeft == null)
        {
            tutorialLeft = transform.FindDeepChild("Tutorial_Left").gameObject;
        }
        commanders = FindObjectOfType<Army>();

    }

    void Update()
    {
        if (!version2)
        {
            if (robot.activeInHierarchy) { summoned = true; }
            if (summoned)
            {
                if (!robot.activeInHierarchy) { tutorialStart = true; }

                if (tutorialStart)
                {
                    if (commanders.currentFighter == null)
                    {
                        tutorialRight.SetActive(true);
                        tutorialLeft.SetActive(false);
                    }
                    else
                    {

                        tutorialRight.SetActive(false);
                        tutorialLeft.SetActive(true);
                    }

                }
            }
        }
        else
        {
            if (!robot.activeInHierarchy && !summoned)
            {
                summoned = true;
                tutorialLeft.SetActive(false);
                tutorialRight.SetActive(true);
            }

            if (summoned)
            {
                if (robot.activeInHierarchy)
                {
                    tutorialRight.SetActive(false);
                }
            }
        }
    }
}
