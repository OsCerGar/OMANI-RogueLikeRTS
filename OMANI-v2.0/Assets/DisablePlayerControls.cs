using UnityEngine;

public class DisablePlayerControls : MonoBehaviour
{

    public CharacterMovement movement;
    public SpriteRenderer MouseSprite, MouseSprite2;
    public Powers powers;
    public Army army;
    float originalspeed;

    private void OnEnable()
    {
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
        if (MouseSprite != null)
        {

            MouseSprite.enabled = false;
            MouseSprite2.enabled = false;
        }
        //disablespowers

        //enablesspowers
        if (powers != null)
        {
            powers.enabled = false;
        }
    }

    private void OnDisable()
    {

        movementSpeedBack();
        if (MouseSprite != null)
        {
            MouseSprite.enabled = true;
            MouseSprite2.enabled = true;
        }

        //enablesspowers
        if (powers != null)
        {
            powers.enabled = true;
        }
    }

    public void movementSpeedBack()
    {
        movement.speed = originalspeed;
    }
}
