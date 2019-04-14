using System.Collections.Generic;
using UnityEngine;

public class FlagThrowing : MonoBehaviour
{

    LookDirectionsAndOrder lookDAO;
    Army army;
    PlayerInputInterface player;

    [SerializeField]
    List<Flag> flags = new List<Flag>();
    Robot robotToThrow = null;
    RadialMenu_GUI radialMenu;

    [SerializeField]
    Sprite flagPointer;
    // Start is called before the first frame update
    void Start()
    {
        lookDAO = FindObjectOfType<LookDirectionsAndOrder>();
        army = FindObjectOfType<Army>();
        player = FindObjectOfType<PlayerInputInterface>();
        radialMenu = FindObjectOfType<RadialMenu_GUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.inputs.GetButtonDown("Throw") && army.getCells()[army.ArmyCellSelected] != null && army.getCells()[army.ArmyCellSelected].getRobotType() != null)
        {
            ThrowDown();
        }
        if (player.inputs.GetButtonUp("Throw") && robotToThrow != null)
        {
            ThrowUp();
        }
    }

    private void ThrowUp()
    {
        lookDAO.pointerDirection.pointerDefault();
        Flag thrownFlag = flags[flags.Count - 1];
        thrownFlag.transform.SetParent(null);
        thrownFlag.transform.position = lookDAO.miradaposition;
        thrownFlag.Thrown(robotToThrow);
        flags.Remove(thrownFlag);
        robotToThrow = null;
    }

    private void ThrowDown()
    {
        lookDAO.pointerDirection.ChangePointer(flagPointer, new Vector3(0.35f, 0.35f, 0.35f));
        if (army.getCells()[army.ArmyCellSelected].getRobotType() != null) { }
        robotToThrow = army.getCells()[army.ArmyCellSelected].GetRobot();
        robotToThrow.Fired();
        robotToThrow.Dematerialize();
        radialMenu.UpdateState();
    }
}
