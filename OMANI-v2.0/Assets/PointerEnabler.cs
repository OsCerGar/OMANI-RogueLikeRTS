using UnityEngine;

public class PointerEnabler : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer pointer;

    private bool state, pressed;

    public bool disablePlayerControl;
    //Inputs
    PlayerInputInterface inputController;
    LookDirectionsAndOrder lookDAO;

    private void OnEnable()
    {
        inputController = FindObjectOfType<PlayerInputInterface>();
        lookDAO = FindObjectOfType<LookDirectionsAndOrder>();
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
        }
    }

    private void Inputs()
    {
        if (lookDAO.playingOnController)
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
    }
    private void PointerState()
    {
        if (pointer != null)
        {
            if (!state)
            {
                pointer.enabled = false;
                state = true;
            }
            else
            {
                pointer.enabled = true;
                state = false;
            }
        }
    }
}
