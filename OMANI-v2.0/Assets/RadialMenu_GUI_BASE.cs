using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu_GUI_BASE : MonoBehaviour
{
    private List<Image> amountOfRobots = new List<Image>();
    private Image Robot;
    private Image selectedVisualFeedback, Energy1FeedBack, Energy2FeedBack, Energy3FeedBack;
    [SerializeField]
    public Material FeedBackON, FeedBackOff;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        DisableAll();
    }

    private void Initialize()
    {

        amountOfRobots.Add(transform.Find("Energy1").GetComponent<Image>());
        amountOfRobots.Add(transform.Find("Energy2").GetComponent<Image>());
        amountOfRobots.Add(transform.Find("Energy3").GetComponent<Image>());
        amountOfRobots.Add(transform.Find("Energy4").GetComponent<Image>());
        selectedVisualFeedback = transform.Find("ExteriorFeedback").GetComponent<Image>();

        Energy1FeedBack = transform.Find("Energy1FeedBack").GetComponent<Image>();
        Energy2FeedBack = transform.Find("Energy2FeedBack").GetComponent<Image>();
        Energy3FeedBack = transform.Find("Energy3FeedBack").GetComponent<Image>();

        Robot = transform.Find("Robot").GetComponent<Image>();
    }

    public void DisableAll()
    {
        foreach (Image arobot in amountOfRobots)
        {
            arobot.enabled = false;
        }
        Robot.enabled = false;
    }

    private List<Image> GetAmountOfRobots()
    {
        return amountOfRobots;
    }

    //Set the robot
    public void UISetRobot()
    {
        Robot.enabled = true;
    }

    public void UISetAmountOfRobots(int _quantityOfRobots)
    {
        DisableAll();

        for (int i = 0; i < _quantityOfRobots; i++)
        {
            amountOfRobots[i].enabled = true;
        }
        UISetRobot();
    }

    public void VisualFeedBack()
    {
        selectedVisualFeedback.material = FeedBackON;
        Energy1FeedBack.material = FeedBackON;
        Energy2FeedBack.material = FeedBackON;
        Energy3FeedBack.material = FeedBackON;

    }
    public void noVisualFeedBack()
    {
        selectedVisualFeedback.material = FeedBackOff;
        Energy1FeedBack.material = FeedBackOff;
        Energy2FeedBack.material = FeedBackOff;
        Energy3FeedBack.material = FeedBackOff;
    }

}
