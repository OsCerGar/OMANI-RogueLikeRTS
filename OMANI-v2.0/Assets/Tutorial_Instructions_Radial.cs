using UnityEngine;

public class Tutorial_Instructions_Radial : MonoBehaviour
{
    public GameObject parent, pcImage, controllerImage, nextTutorial;
    [SerializeField]
    Transform look;

    bool pc = false;
    private void Awake()
    {
        if (look == null) { look = FindObjectOfType<LookDirectionsAndOrder>().pointerDirection.transform; }
    }

    private void LateUpdate()
    {
        if (pc)
        {
            if (!Input.GetMouseButton(2))
            {
                PCVersion();
            }
            /*
            else
            {
                parent.SetActive(false);
            }
            */
        }

        if (Input.GetMouseButtonDown(2))
        {
            pc = true;
            PCVersion();
        }

        if (Input.GetAxis("L2") > 0.5f)
        {
            pc = false;
            nextTutorial.SetActive(true);
        }

        if (!pc)
        {

            if (Input.GetAxis("L2") < 0.5f)
            {

                ControllerVersion();
                pc = false;
            }
            /*
            else
            {
                parent.SetActive(false);
            }
            */
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
