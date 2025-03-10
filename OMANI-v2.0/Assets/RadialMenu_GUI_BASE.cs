﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu_GUI_BASE : MonoBehaviour
{
    [SerializeField]
    private List<RadialMenuFeedback> amountOfRobots = new List<RadialMenuFeedback>();
    public List<Sprite> robotTypes = new List<Sprite>();
    private string robotType;
    public Image Robot;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        DisableAll();
    }

    private void Initialize()
    {

        amountOfRobots.Add(transform.Find("0Robot").GetComponent<RadialMenuFeedback>());
        amountOfRobots.Add(transform.Find("1Robot").GetComponent<RadialMenuFeedback>());
        amountOfRobots.Add(transform.Find("2Robot").GetComponent<RadialMenuFeedback>());
        amountOfRobots.Add(transform.Find("3Robot").GetComponent<RadialMenuFeedback>());
        amountOfRobots.Add(transform.Find("4Robot").GetComponent<RadialMenuFeedback>());

    }

    public void DisableAll()
    {
        foreach (RadialMenuFeedback arobot in amountOfRobots)
        {
            arobot.gameObject.SetActive(false);
        }
    }

    public List<RadialMenuFeedback> GetAmountOfRobots()
    {
        return amountOfRobots;
    }

    public string GetRobotType()
    {
        return robotType;
    }
    //Set the robot
    public void UISetRobot(string _robotType)
    {
        robotType = _robotType;
        bool found = false;
        if (_robotType != null)
        {
            foreach (Sprite robotSprite in robotTypes)
            {
                if (robotSprite.name.Equals(_robotType))
                {
                    found = true;
                    if (Robot != null)
                    {
                        Robot.enabled = true;
                        Robot.sprite = robotSprite;
                    }
                }
            }

            if (!found)
            {
                Debug.Log("UI : RobotSprite not found, ¿Missing Sprite, wrong name?");
            }
        }
        else
        {
            if (Robot != null)
            {

                Robot.enabled = false;
            }
        }
    }

    public void UISetAmountOfRobots(int _quantityOfRobots)
    {
        DisableAll();
        amountOfRobots[_quantityOfRobots].gameObject.SetActive(true);
    }

    public void VisualFeedBack()
    {
        foreach (RadialMenuFeedback arobot in amountOfRobots)
        {
            if (arobot.gameObject.activeSelf) { arobot.FeedbackOn(); }
        }
    }

    public void noVisualFeedBack()
    {
        foreach (RadialMenuFeedback arobot in amountOfRobots)
        {
            arobot.FeedbackOff();
        }
    }

}
