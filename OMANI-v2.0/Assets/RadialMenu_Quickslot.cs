using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu_Quickslot : MonoBehaviour
{
    public Image background, robot;
    public List<Sprite> robotTypes = new List<Sprite>();
    public Sprite backgroundNormal, backgroundSelected;
    //Set the robot
    public void UISetRobot(string _robotType)
    {
        bool found = false;
        if (_robotType != null)
        {
            foreach (Sprite robotSprite in robotTypes)
            {
                if (robotSprite.name.Equals(_robotType))
                {
                    found = true;
                    robot.enabled = true;
                    robot.sprite = robotSprite;
                }
            }

            if (!found)
            {
                Debug.Log("UI : RobotSprite not found, ¿Missing Sprite, wrong name?");
            }
        }
        else
        {
            robot.enabled = false;
        }
    }
}
