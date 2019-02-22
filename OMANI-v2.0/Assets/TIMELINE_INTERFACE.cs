using UnityEngine;
using UnityEngine.Playables;

public class TIMELINE_INTERFACE : MonoBehaviour
{
    public CharacterMovement movement;
    public OMANINPUT controls;
    public SpriteRenderer MouseSprite, MouseSprite2;
    public Powers powers;

    public PlayableDirector timeline;

    public void TPlay() { timeline.Play(); }
    public void TStop() { timeline.Stop(); }


    public void EnableControls(bool _isSpeedBack)
    {
        if (_isSpeedBack) { movementSpeedBack(); }
        MouseSprite.enabled = true;
        MouseSprite2.enabled = true;

        //enablesspowers
        powers.enabled = true;

        //Enables Orders
        controls.PLAYER.Order.Enable();
    }

    public void movementSpeedBack()
    {
        movement.speed = 0.15f;
    }

    public void DisableControls()
    {
        movement.speed = 0;
        MouseSprite.enabled = false;
        MouseSprite2.enabled = false;
        //disablespowers

        powers.enabled = false;

        //Disables Orders
        controls.PLAYER.Order.Disable();
    }
}
