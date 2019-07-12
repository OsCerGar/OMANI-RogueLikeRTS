using UnityEngine;

public class Tutorials_Robot : MonoBehaviour
{
    [SerializeField]
    GameObject summonTutorial, spinTutorial, disablePlayer;
    Army commanders;


    [SerializeField]
    GameObject robot;
    bool summoned, tutorialStart;

    public bool sumonTutorialDone;
    PlayerInputInterface input;
    private void Start()
    {
        input = FindObjectOfType<PlayerInputInterface>();
    }
    void Update()
    {

        if (robot.activeInHierarchy) { summoned = true; }

        if (!robot.activeInHierarchy && summoned && !sumonTutorialDone)
        {
            summonTutorial.SetActive(true); //disablePlayer.SetActive(true);
        }
        if (robot.activeInHierarchy && summoned && sumonTutorialDone)
        {
            summonTutorial.SetActive(false); spinTutorial.SetActive(true); //disablePlayer.SetActive(true); 
        }

        if (input.Summon) { SumonTutorialDone(); }
    }

    public void SumonTutorialDone()
    {
        sumonTutorialDone = true;
    }
}