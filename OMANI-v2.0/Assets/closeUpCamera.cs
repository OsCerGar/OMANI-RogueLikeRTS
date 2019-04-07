using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
public class closeUpCamera : MonoBehaviour
{
    CharacterMovement movementController;
    public CinemachineVirtualCamera closeCamera, playerCamera;
    public CinemachineBrain brain;
    public List<CinemachineVirtualCamera> cinematicCameras;
    bool closedUp;

    bool stop;
    // Start is called before the first frame update
    void Awake()
    {
        movementController = FindObjectOfType<CharacterMovement>();
        brain = FindObjectOfType<CinemachineBrain>();
        if (closeCamera == null || playerCamera == null) { stop = true; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stop)
        {
            if (movementController.onNoMovementTime > 15f)
            {
                closedUp = true;

                /*
                if (cinematicCameras.Count > 0)
                {
                    foreach (CinemachineVirtualCamera camera in cinematicCameras)
                    {
                        Debug.Log("cine " + brain.ActiveVirtualCamera.VirtualCameraGameObject.name);
                        Debug.Log("no " + camera.gameObject.name);
                        if (brain.ActiveVirtualCamera.VirtualCameraGameObject == camera.gameObject) { Debug.Log("really");  closedUp = false; }
                    }
                }
                */

                if (closedUp == true)
                {
                    if (playerCamera.enabled)
                    {

                        playerCamera.enabled = false;
                        closeCamera.enabled = true;
                    }

                }
            }
            else
            {
                if (closedUp)
                {

                    closedUp = false;
                    if (!playerCamera.enabled)
                    {

                        playerCamera.enabled = true;
                        closeCamera.enabled = false;

                    }

                }
            }

        }


    }
}

