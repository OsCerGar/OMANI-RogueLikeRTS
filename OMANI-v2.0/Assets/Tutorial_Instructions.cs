using UnityEngine;

public class Tutorial_Instructions : MonoBehaviour
{
    public OMANINPUT controls;
    public GameObject parent, pcImage, controllerImage;
    [SerializeField]
    Transform look;


    private void Awake()
    {
        if (look == null) { look = FindObjectOfType<LookDirectionsAndOrder>().pointerDirection.transform; }
        controls.PLAYER.WASD.performed += movement => PCVersion();
        controls.PLAYER.Joystick.performed += Controllermovement => ControllerVersion();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(look.transform.position, transform.position) < 3f)
        {
            parent.SetActive(true);

        }
        else
        {
            parent.SetActive(false);
        }

    }

    void PCVersion()
    {
        pcImage.SetActive(true);
        controllerImage.SetActive(false);
    }
    void ControllerVersion()
    {
        pcImage.SetActive(false);
        controllerImage.SetActive(true);
    }
}
