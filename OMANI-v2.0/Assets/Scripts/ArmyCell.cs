using System.Collections.Generic;
using UnityEngine;

public class ArmyCell : MonoBehaviour
{
    [SerializeField]
    private List<Robot> robots = new List<Robot>();
    private int limit = 5;
    private string robotType = null;
    RadialMenu_GUI radialMenu;

    private void Start()
    {
        radialMenu = FindObjectOfType<RadialMenu_GUI>();
    }

    public void setRobotType(Robot _robot)
    {
        robotType = _robot.boyType;
        Transaction();
    }

    public string getRobotType()
    {
        return robotType;
    }

    public int getRobotQuantity()
    {
        return robots.Count;
    }

    public bool availableArmyCellSpace()
    {
        if (robots.Count + 1 <= limit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool availableEmptySpace()
    {
        if (robots.Count == 0)
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    public bool addRobot(Robot _robot)
    {
        if (robots.Count + 1 <= limit)
        {
            if (robots.Count == 0)
            {
                setRobotType(_robot);
            }

            robots.Add(_robot);
            Transaction();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void removeRobot(Robot _robot)
    {
        robots.Remove(_robot);

        if (robots.Count == 0)
        {
            Clean();
        }
        //Transaction();
    }

    public Robot GetRobot()
    {
        return robots[0];
    }

    public void Clean()
    {
        robots.Clear();
        robotType = null;
    }

    //Changes made to the ArmyCell, used to Update UI
    public void Transaction()
    {
        radialMenu.UpdateState();
    }
}
