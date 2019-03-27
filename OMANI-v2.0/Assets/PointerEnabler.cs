using UnityEngine;

public class PointerEnabler : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer pointer, dots;

    private bool state, pressed;

    public bool disablePlayerControl;

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
        
        if (!Input.GetMouseButton(0))
        {
            pointer.enabled = true;
            dots.enabled = true;
            state = false;
        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            pointer.enabled = false;
            dots.enabled = false;
            state = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointer.enabled = true;
            dots.enabled = true;
            state = false;
        }
        */
        if (Input.GetAxis("R2") > 0.5f)
        {
            if (!pressed)
            {
                PointerState();
                pressed = true;
            }
        }
        if (Input.GetAxis("R2") < 0.5f)
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
