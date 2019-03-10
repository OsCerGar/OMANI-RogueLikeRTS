using UnityEngine;

public class PointerEnabler : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer pointer, dots;

    [SerializeField]
    OMANINPUT controls;
    bool state;
    private void Awake()
    {
        controls.PLAYER.OrderLaser.performed += context => PointerState();
    }

    private void PointerState()
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
