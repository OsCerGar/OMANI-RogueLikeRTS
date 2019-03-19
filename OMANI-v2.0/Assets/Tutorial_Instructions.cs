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
        if (pc)
        {
            if (!Input.GetMouseButton(input))
            {
                PCVersion();
            }
            else
            {
                parent.SetActive(false);
            }
        }

        if (input == 0)
        {
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
        }
        if (input == 1)
        {
            if (Input.GetAxis("L2") > 0.5f)
            {
                pc = false;
            }
            if (!pc)
            {

                if (Input.GetAxis("L2") > 0.5f)
                {

                    ControllerVersion();
                    pc = false;
                }
                else
                {

                    parent.SetActive(false);
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
