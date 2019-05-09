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

    //ArmyCellSelection
    private RadialMenu_GUI radialMenu;
    LookDirectionsAndOrder look;
    public Robot currentFighter;

    public int ArmyCellSelected;


    [SerializeField] bool radialMenuEnabled = true;

    Power_Laser power_Laser;
    Powers powers;
    private bool pressedR2, pressedL2, pressedMouse;
    //Inputs 
    PlayerInputInterface player;

    [SerializeField]
    AudioSource summonAndCant;


    private void Start()
    {
        look = FindObjectOfType<LookDirectionsAndOrder>();
        player = FindObjectOfType<PlayerInputInterface>();
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
            if (player.Laser)
            {
                if (!pressedR2)
                {
                    Order(); pressedR2 = true;
                }
            }
            if (!player.Laser)
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
        if (radialMenuEnabled)
        {
            if (player.inputs.GetButtonDown("Radial Menu")) { if (!pressedL2) { radialMenuPopUp(); pressedL2 = true; } }
            if (player.inputs.GetButtonUp("Radial Menu")) { if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; } }

            //Esto tiene que ser la flechita de la opción seleccionada, sino, selecciona otra cosa del menu radial.
            //if (player.inputs.GetButtonUp("IndexUp")) { if (pressedL2) { IndexUp(); } }
            if (player.inputs.GetButtonDown("FireLaser")) { if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; } }

            if (player.RobotQuickSelection.x == 1)
            {
                if (armyCell[1].getRobotType() != null)
                {
                    radialMenu.menuItem(1); if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; }
                }
            }
            if (player.RobotQuickSelection.x == -1)
            {
                if (armyCell[3].getRobotType() != null)
                {
                    radialMenu.menuItem(3); if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; }
                }
            }
            if (player.RobotQuickSelection.y == 1)
            {
                if (armyCell[0].getRobotType() != null)
                {
                    radialMenu.menuItem(0); if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; }
                }
            }
            if (player.RobotQuickSelection.y == -1)
            {
                if (armyCell[2].getRobotType() != null)
                {
                    radialMenu.menuItem(2); if (pressedL2) { radialMenuPopDown(null); pressedL2 = false; }
                }
            }
        }

        if (player.inputs.GetButtonDown("Summon")) { SummonRobot(); }

    }

    private void IndexUp()
    {
        armyCell[ArmyCellSelected].IndexUp();
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
            if (armyCell[radialMenu.PopDown()].getRobotType() != null)
            {

                newArmyCellSelected = radialMenu.PopDown();
            }
            else { newArmyCellSelected = 4; } 
        }
        else
        {
            newArmyCellSelected = (int)_newSelected;
        }

        // Debería deseleccionar.
        //If currentFighter tiene toda la energia, debería desmaterializarse y volver.
        //Si no, debería desconectarse.
        string robotTypes = null;

        if (newArmyCellSelected != 4)
        {
            robotTypes = armyCell[newArmyCellSelected].getRobotType();
        }

        if (currentFighter != null && robotTypes != currentFighter.boyType && robotTypes != null)
        {

            ArmyCellSelected = newArmyCellSelected;
            Robot _transitionStateRobot = currentFighter;
            Remove(currentFighter);
            Reclute(_transitionStateRobot);
            SummonRobot();
        }

        else if (robotTypes != null)
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

    public ArmyCell findArmyCellWithRobots()
    {
        ArmyCell cellWithRobot = null;

        //Checks if there is already an ArmyCell with RobotType, or if there is space for a new one
        foreach (ArmyCell cell in armyCell)
        {
            if (cell.getRobotType() != null)
            {
                cellWithRobot = cell;

            }
        }

        return cellWithRobot;
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
        //selects the robot
        if (armyCell[ArmyCellSelected].getRobotType() == null && currentFighter == null)
        {
            ArmyCellSelected = armyCell.FindIndex(item => item == cellToSaveRobot);
            radialMenu.SetCurrentMenuItem(ArmyCellSelected);
        }
        armyCell[ArmyCellSelected].Transaction();
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
            look.AlternativeCenter(null);

        }

        else
        {
            //If something is selected
            if (ArmyCellSelected != 4 && armyCell[ArmyCellSelected].getRobotType() != null)
            {

                //Materialice the next one
                currentFighter = armyCell[ArmyCellSelected].GetRobot();
                currentFighter.Materialize(ShootingPosition, look.pointerDirection.gameObject);
                look.AlternativeCenter(currentFighter.transform);

                //Recluted false so if he dies, he goes back to the pool
                currentFighter.recluted = false;

                //Removes from list to update UI, still currentFighter
                //RemoveWithoutFighter(currentFighter);
                //armyCell[ArmyCellSelected].Transaction();
                player.SetVibration(0, 0.25f, 0.25f, false);
            }
            else
            {
                if (!summonAndCant.isPlaying)
                {
                    summonAndCant.Play();
                }
            }

        }
    }

    public void Remove(Robot _robot)
    {
        if (_robot == currentFighter)
        {
            currentFighter = null;
            look.AlternativeCenter(null);
            //here
            armyCell[ArmyCellSelected].Transaction();
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