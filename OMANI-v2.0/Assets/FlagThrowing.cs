using System.Collections.Generic;
using UnityEngine;

public class FlagThrowing : MonoBehaviour
{

    LookDirectionsAndOrder lookDAO;
    Army army;
    PlayerInputInterface player;
    Animator anim;
    [SerializeField]
    List<Flag> flags = new List<Flag>();
    Robot robotToThrow = null;
    RadialMenu_GUI radialMenu;

    [SerializeField]
    Sprite flagPointer;

    bool throwing, sweetSpot = false;

    float originalViewRadius, expValue;


    // Start is called before the first frame update
    void Start()
    {
        lookDAO = FindObjectOfType<LookDirectionsAndOrder>();
        army = FindObjectOfType<Army>();
        player = FindObjectOfType<PlayerInputInterface>();
        radialMenu = FindObjectOfType<RadialMenu_GUI>();
        anim = GetComponentInChildren<Animator>();
        originalViewRadius = lookDAO.viewRadius;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.inputs.GetButtonDown("Throw") && army.getCells()[army.ArmyCellSelected] != null && army.getCells()[army.ArmyCellSelected].getRobotType() != null)
        {
            ThrowDown();
            throwing = true;
            expValue = 0;
        }
        if (throwing)
        {
            ThrowingDown();
        }
        if (player.inputs.GetButtonUp("Throw") && robotToThrow != null)
        {
            ThrowUp();
            throwing = false;
        }
    }

    private void ThrowingDown()
    {
        //Tiene que empezar a mover el miradaposition entre el jugador hasta la dirección fija
        expValue += 2.5f * Time.deltaTime;
        lookDAO.viewRadius += 0.5f * expValue;
        lookDAO.viewRadius = Mathf.Clamp(lookDAO.viewRadius, 1.5f, originalViewRadius + 2f);
    }

    private void ThrowUp()
    {
        //Lo suelta en el punto guardado.
        lookDAO.pointerDirection.pointerDefault();
        Flag thrownFlag = flags[flags.Count - 1];
        thrownFlag.transform.SetParent(null);
        thrownFlag.transform.position = lookDAO.miradaposition;
        thrownFlag.Thrown(robotToThrow);
        flags.Remove(thrownFlag);
        robotToThrow = null;

        //restores view radius
        lookDAO.viewRadius = originalViewRadius;

        anim.SetTrigger("TriggerUp");

    }

    private void ThrowDown()
    {
        lookDAO.pointerDirection.ChangePointer(flagPointer, new Vector3(0.35f, 0.35f, 0.35f));
        if (army.getCells()[army.ArmyCellSelected].getRobotType() != null) { }
        robotToThrow = army.getCells()[army.ArmyCellSelected].GetRobot();
        robotToThrow.Fired();
        robotToThrow.Dematerialize();
        radialMenu.UpdateState();

        //Starts moving position
        lookDAO.viewRadius = 0.5f;

        anim.SetTrigger("TriggerDown");
    }

    private void SweetSpot()
    {
        //SweetSpot reversal, animation calls it
        if (!sweetSpot)
        {
            sweetSpot = true;
            player.SetVibration(2, 1f, 0.1f, false);
        }
        else if (sweetSpot)
        {
            sweetSpot = false;
        }
        Debug.Log(sweetSpot);

    }
}
