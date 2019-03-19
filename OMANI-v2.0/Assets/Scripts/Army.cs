using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [Space]
    [SerializeField]
    private List<ArmyCell> armyCell = new List<ArmyCell>();

    [SerializeField]
    private GameObject OrderPositionObject;

    private List<GameObject> positions = new List<GameObject>();
    [SerializeField] private GameObject ShootingPosition;
    LookDirectionsAndOrder look;

    //ArmyCellSelection
    private RadialMenu_GUI radialMenu;
    private int ArmyCellSelected;

    public Robot currentFighter;

    bool radialMenuOn;

    Power_Laser power_Laser;
    Powers powers;
    private bool pressedR2, pressedL2, pressedMouse;


    [SerializeField]
    AudioSource summonAndCant;

    private void Start()
    {
        look = FindObjectOfType<LookDirectionsAndOrder>();
        powers = FindObjectOfType<Powers>();
        power_Laser = FindObjectOfType<Power_Laser>();
        radialMenu = FindObjectOfType<RadialMenu_GUI>();
    }
    private void Update()
    {
        Inputs();

        if (currentFighter != null)
        {

            //EMIT LASER
            power_Laser.EmitLaser(true, currentFighter.ball);

            //si current fighter pasa a ser null, y no se ha hecho U. Se queda colgao
            if (Input.GetMouseButtonDown(0)) { if (!pressedMouse) { Order(); pressedMouse = true; } }
            if (Input.GetAxis("R2") > 0.5f)
            {
                if (!pressedR2)
                {
                    Order(); pressedR2 = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (pressedMouse)
                {
                    Order();
                    pressedMouse = false;
                }
            }
            if (Input.GetAxis("R2") < 0.5f)
            {
                if (pressedR2)
                {
                    Order();
                    pressedR2 = false;
                }
            }
        }

    }

    private void Inputs()
    {
        if (Input.GetAxis("L2") > 0.5f) { if (!pressedL2) { radialMenuPopUp(); pressedL2 = true; } }
        if (Input.GetAxis("L2") < 0.25f) { if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; } }
        if (Input.GetMouseButtonDown(2)) { radialMenuPopUp(); }
        if (Input.GetMouseButtonUp(2)) { radialMenuPopDown(null); }
        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Summon")) { SummonRobot(); }

    }

    //Makes the RadialMenu Visible
    private void radialMenuPopUp()
    {
        radialMenu.PopUp();
    }

    //Makes the RadialMenu invisible and gets the selected menu item.
    public void radialMenuPopDown(int? _newSelected)
    {
        int newArmyCellSelected;

        if (_newSelected == null)
        {
            newArmyCellSelected = radialMenu.PopDown();
        }
        else
        {
            newArmyCellSelected = (int)_newSelected;
        }

        // Debería deseleccionar.
        //If currentFighter tiene toda la energia, debería desmaterializarse y volver.
        //Si no, debería desconectarse.

        string robotTypes = armyCell[newArmyCellSelected].getRobotType();

        if (robotTypes == null)
        {
            ArmyCellSelected = 4;

            if (currentFighter != null)
            {
                Robot _transitionStateRobot = currentFighter;
                Remove(currentFighter);
                Reclute(_transitionStateRobot);
            }

        }

        else if (currentFighter != null && robotTypes != currentFighter.boyType)
        {
            ArmyCellSelected = newArmyCellSelected;

            Robot _transitionStateRobot = currentFighter;
            Remove(currentFighter);
            Reclute(_transitionStateRobot);
        }

        else
        {
            ArmyCellSelected = newArmyCellSelected;
        }


    }

    public ArmyCell checkArmyCellAvailable(Robot _newRobot)
    {
        ArmyCell availableCell = null;
        bool cellFound = false; //If there are cells with already a robot on it == true

        //Checks if there is already an ArmyCell with RobotType, or if there is space for a new one
        foreach (ArmyCell cell in armyCell)
        {
            if (cell.getRobotType() != null && cell.getRobotType().Equals(_newRobot.boyType))
            {
                if (checkArmyCellsSpace(cell))
                {

                    availableCell = cell;
                    cellFound = true;
                }
            }
        }

        //Checks if there are empty Cells
        if (!cellFound)
        {
            foreach (ArmyCell cell in armyCell)
            {
                if (cell.availableEmptySpace() && availableCell == null) //Means that the cell is empty.
                {
                    availableCell = cell;
                }
            }
        }
        return availableCell;
    }
    public bool checkArmyCellsSpace(ArmyCell _cell)
    {
        //Checks if there is space in available ArmyCells of the RobotType
        if (_cell.availableArmyCellSpace())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<ArmyCell> getCells()
    {
        return armyCell;
    }

    //Adds the boy to the Army and makes it follow the Army commander.
    public void Reclute(Robot _robot)
    {

        ArmyCell cellToSaveRobot;
        cellToSaveRobot = checkArmyCellAvailable(_robot);

        if (cellToSaveRobot != null)
        {
            cellToSaveRobot.addRobot(_robot);

            //Recluted so the pool knows.
            _robot.recluted = true;

            //Disable robot
            _robot.Dematerialize();
        }
        else
        {
            //no space left sound or whatever
        }
    }

    public void Order()
    {
        //Makes a Robot Appear if there is no other robot doing stuff.
        //Attacks
        currentFighter.FighterAttack(look.pointerDirection.gameObject);

    }

    public void SummonRobot()
    {
        pressedL2 = false;
        pressedR2 = false;
        pressedMouse = false;
        if (currentFighter != null)
        {
            //Attacks
            Robot _transitionStateRobot = currentFighter;
            Remove(currentFighter);
            Reclute(_transitionStateRobot);
        }

        else
        {
            if (!summonAndCant.isPlaying)
            {
                summonAndCant.Play();
            }
            //If something is selected
            if (ArmyCellSelected != 4 && armyCell[ArmyCellSelected].getRobotType() != null)
            {
                //Materialice the next one
                currentFighter = armyCell[ArmyCellSelected].GetRobot();
                currentFighter.Materialize(ShootingPosition, look.pointerDirection.gameObject);

                //Recluted false so if he dies, he goes back to the pool
                currentFighter.recluted = false;

                //Removes from list to update UI, still currentFighter
                RemoveWithoutFighter(currentFighter);
                armyCell[ArmyCellSelected].Transaction();
            }

        }
    }

    public void Remove(Robot _robot)
    {
        if (_robot == currentFighter)
        {
            currentFighter = null;
        }
        foreach (ArmyCell cell in armyCell)
        {
            cell.removeRobot(_robot);
        }
    }
    public void RemoveWithoutFighter(Robot _robot)
    {
        foreach (ArmyCell cell in armyCell)
        {
            cell.removeRobot(_robot);
        }
    }
}