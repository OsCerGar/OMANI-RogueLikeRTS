using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerControls : MonoBehaviour
{

    public CharacterMovement movement;
    public OMANINPUT controls;
    public SpriteRenderer MouseSprite, MouseSprite2;
    public Powers powers;

    private void OnEnable()
    {
        movement.speed = 0;
        MouseSprite.enabled = false;
        MouseSprite2.enabled = false;
        //disablespowers

        powers.enabled = false;

        //Disables Orders
        controls.PLAYER.OrderLaser.Disable();

    }

    private void OnDisable()
    {
       
         movementSpeedBack();
        MouseSprite.enabled = true;
        MouseSprite2.enabled = true;

        //enablesspowers
        powers.enabled = true;

        //Enables Orders
        controls.PLAYER.OrderLaser.Enable();
    }
    public void movementSpeedBack()
    {
        movement.speed = 0.15f;
    }
}
