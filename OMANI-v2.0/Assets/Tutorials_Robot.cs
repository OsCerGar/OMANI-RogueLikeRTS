using UnityEngine;

public class Tutorials_Robot : MonoBehaviour
{
    GameObject tutorialRight, tutorialLeft;
    Army commanders;


    [SerializeField]
    GameObject robot;
    bool summoned, tutorialStart;
    // Start is called before the first frame update
    void Awake()
    {
        tutorialRight = transform.FindDeepChild("Tutorial_Right").gameObject;
        tutorialLeft = transform.FindDeepChild("Tutorial_Left").gameObject;
        commanders = FindObjectOfType<Army>();
    }

    void Update()
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
}
