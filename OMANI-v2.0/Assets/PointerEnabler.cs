using UnityEngine;

public class PointerEnabler : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer pointer, dots;

    private bool state, pressed;

    public bool disablePlayerControl;
    //Inputs
    PlayerInputInterface inputController;

    private void OnEnable()
    {
        inputController = FindObjectOfType<PlayerInputInterface>();
    }
    private void Update()
    {
        if (!disablePlayerControl)
        {
            Inputs();
        }
        else
        {
            pointer.enabled = false;
            dots.enabled = false;
        }
    }

    private void Inputs()
    {

        if (inputController.Laser)
        {
            if (!pressed)
            {
                PointerState();
                pressed = true;
            }
        }
        if (!inputController.Laser)
        {
            if (pressed)
            {
                PointerState();
                pressed = false;
            }
        }
    }
    private void PointerState()
    {
        if (pointer != null)
        {
            if (!state)
            {
                pointer.enabled = false;
                dots.enabled = false;
                state = true;
            }
            else
            {
                pointer.enabled = true;
                dots.enabled = true;
                state = false;
            }
        }
    }
}
