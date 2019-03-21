using System.Collections.Generic;
using UnityEngine;
public class DisablePlayerControls : MonoBehaviour
{

    public CharacterMovement movement;
    public PointerEnabler pointer;
    public Powers powers;
    public Army army;
    float originalspeed;

    public List<Canvas> canvas;

    [SerializeField]
    GameObject extras;

    private void OnEnable()
    {
        pointer = FindObjectOfType<PointerEnabler>();
        //!!!!!!!!!!!
        if (army == null)
        {
            army = FindObjectOfType<Army>();
        }
        if (army.enabled == true)
        {
            if (army.currentFighter != null) { army.SummonRobot(); }
        }

        originalspeed = movement.speed;
        movement.speed = 0;
        pointer.disablePlayerControl = true;

        if (powers != null)
        {
            powers.enabled = false;
        }
        if (canvas != null && canvas.Count > 0)
        {

            foreach (Canvas canv in canvas)
            {
                canv.enabled = false;
            }
        }
        if (extras != null) { extras.SetActive(false); }
    }

    private void OnDisable()
    {

        movementSpeedBack();
        pointer.disablePlayerControl = false;


        //enablesspowers
        if (powers != null)
        {
            powers.enabled = true;
        }

        if (canvas != null && canvas.Count > 0)
        {
            foreach (Canvas canv in canvas)
            {
                canv.enabled = true;
            }
        }
        if (extras != null) { extras.SetActive(true); }

    }

    public void movementSpeedBack()
    {
        movement.speed = originalspeed;
    }
}
