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
    private void OnEnable()
    {
        controls.PLAYER.OrderLaser.Enable();
    }
    private void OnDisable()
    {
        controls.PLAYER.OrderLaser.Disable();
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
