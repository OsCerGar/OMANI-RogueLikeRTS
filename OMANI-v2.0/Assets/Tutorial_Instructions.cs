using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Instructions : MonoBehaviour
{
    public OMANINPUT controls;
    public Sprite pcImage, controllerImage;
    public Image instructionImage;

    private void Awake()
    {
        controls.PLAYER.WASD.performed += movement => PCVersion();
        controls.PLAYER.Joystick.performed += Controllermovement => ControllerVersion();
    }

    void PCVersion()
    {
        instructionImage.sprite = pcImage;
    }
    void ControllerVersion()
    {
        instructionImage.sprite = controllerImage;
    }
}
