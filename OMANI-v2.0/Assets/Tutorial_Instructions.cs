using UnityEngine;

public class Tutorial_Instructions : MonoBehaviour
{
    public GameObject parent, pcImage, controllerImage;
    [SerializeField]
    Transform look;
    [SerializeField]
    int input = 0;

    bool pc = true;

    private void Awake()
    {
        if (look == null) { look = FindObjectOfType<LookDirectionsAndOrder>().pointerDirection.transform; }
    }

    private void LateUpdate()
    {


        if (input == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pc = true;
                PCVersion();
            }

            if (Input.GetAxis("R2") > 0.5f)
            {
                pc = false;
            }
            if (!pc)
            {
                if (Input.GetAxis("R2") < 0.5f)
                {
                    ControllerVersion();
                }
                else
                {
                    parent.SetActive(false);
                }
            }

            if (pc)
            {
                if (!Input.GetMouseButton(0))
                {
                    PCVersion();
                }
                else
                {
                    parent.SetActive(false);
                }
            }
        }
        if (input == 1)
        {
            if (Input.GetMouseButtonDown(2))
            {
                pc = true;
                PCVersion();
            }

            if (Input.GetAxis("L2") > 0.5f)
            {
                pc = false;
            }

            if (!pc)
            {

                if (Input.GetAxis("L2") < 0.5f)
                {

                    ControllerVersion();
                    pc = false;
                }
                else
                {
                    parent.SetActive(false);
                }
            }

            if (pc)
            {
                if (!Input.GetMouseButton(2))
                {
                    PCVersion();
                }
                else
                {
                    parent.SetActive(false);
                }
            }

        }
        if (input == 2)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                pc = true;
                PCVersion();
            }

            if (Input.GetAxis("HorizontalJoystick") > 0.2f || Input.GetAxis("HorizontalJoystick") < -0.2f || Input.GetAxis("VerticalJoystick") > 0.2f || Input.GetAxis("VerticalJoystick") < -0.2f)
            {
                pc = false;
            }

            if (!pc)
            {

                if (Input.GetAxis("HorizontalJoystick") < 0.2f || Input.GetAxis("HorizontalJoystick") > -0.2f || Input.GetAxis("VerticalJoystick") < 0.2f || Input.GetAxis("VerticalJoystick") > -0.2f)
                {

                    ControllerVersion();
                    pc = false;
                }
                else
                {
                    parent.SetActive(false);
                }
            }

            if (pc)
            {
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    parent.SetActive(false);
                }
                else
                {
                    PCVersion();
                }
            }
        }

        /*if()
        {
            ControllerVersion();
        }*/
    }

    void PCVersion()
    {
        parent.SetActive(true);

        pcImage.SetActive(true);
        controllerImage.SetActive(false);
    }
    void ControllerVersion()
    {
        parent.SetActive(true);
        pcImage.SetActive(false);
        controllerImage.SetActive(true);
    }
}
