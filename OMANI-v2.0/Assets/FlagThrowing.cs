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
    public static FlagThrowing flagThrowing;
    private void Awake()
    {
        if (flagThrowing == null)
        {
            flagThrowing = this;
        }

    }
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
        Flag thrownFlag = flags[flags.Count - 1];
        thrownFlag.transform.SetParent(null);
        thrownFlag.transform.position = lookDAO.miradaposition;
        thrownFlag.Thrown(robotToThrow);
        flags.Remove(thrownFlag);
        robotToThrow = null;

        //restores view radius
        lookDAO.viewRadius = originalViewRadius;
        lookDAO.ControllerFreeMode();
        //Lo suelta en el punto guardado.
        lookDAO.pointerDirection.pointerDefault();

        anim.SetTrigger("TriggerUp");

    }

    private void ThrowDown()
    {
        if (army.getCells()[army.ArmyCellSelected].getRobotType() != null) { }
        robotToThrow = army.getCells()[army.ArmyCellSelected].GetRobot();
        robotToThrow.Fired();
        robotToThrow.Dematerialize();
        radialMenu.UpdateState();

        //Changes the pointer
        lookDAO.pointerDirection.ChangePointer(flagPointer, new Vector3(0.35f, 0.35f, 0.35f), Color.white);
        lookDAO.pointerDirection.FlagThrowing();
        //Starts moving position
        lookDAO.viewRadius = 0.5f;
        lookDAO.ControllerFreeMode();

        anim.SetTrigger("TriggerDown");
    }

    public void PickUpFlag(Flag _flagToPickUp)
    {
        _flagToPickUp.transform.SetParent(transform);
        _flagToPickUp.transform.position = new Vector3(0, 0, 0);
        flags.Add(_flagToPickUp);
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

    }
}
