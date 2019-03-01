using System.Collections;
using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public OMANINPUT controls;
    public CharacterMovement movement;

    [SerializeField] private InverseKinematics leg1, leg2, leg3, leg4;
    int releasedLegs = 0;
    bool cameraChanged, surkaSpawned;
    TIMELINE_INTERFACE timeline_interface;

    [SerializeField] GameObject StartingCamera, mouse;
    //Wind sound effect
    [SerializeField]
    AudioSource wind;
    [SerializeField]
    float finalVolume;

    float currentLerpTime, lerpTime = 1f;
    bool windDown;

    private void Awake()
    {
        timeline_interface = GetComponent<TIMELINE_INTERFACE>();

        // ESTO LLAMA A LA CAMERACHANGE
        controls.PLAYER.WASD.performed += movement => CameraChange();
        controls.PLAYER.Joystick.performed += Controllermovement => CameraChange();
        controls.PLAYER.LASERZONE.performed += context => CameraChange();
        controls.PLAYER.LASERZONERELEASE.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONGPREPARATION.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONG.performed += context => CameraChange();
        mouse.SetActive(false);
        controls.PLAYER.RadialMenuUp.Disable();
        controls.PLAYER.RadialMenuDown.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartRestrictions();
    }

    private void Update()
    {
        if (windDown)
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.0025f);

            wind.volume = Mathf.Lerp(wind.volume, finalVolume, t);
        }
    }
    private void StartRestrictions()
    {
        controls.PLAYER.LASERZONE.Enable();
        controls.PLAYER.LASERZONERELEASE.Enable();
        controls.PLAYER.LASERSTRONGPREPARATION.Enable();
        controls.PLAYER.LASERSTRONG.Enable();
    }

    public void LegRelease(int _leg)
    {
        switch (_leg)
        {
            case 1:
                //Activate Leg
                leg1.enabled = true;
                break;
            case 2:
                leg2.enabled = true;
                break;
            case 3:
                leg3.enabled = true;
                break;
            case 4:
                leg4.enabled = true;
                break;
        }
        releasedLegs++;

        if (!surkaSpawned)
        {
            if (releasedLegs > 2)
            {
                SurkaEntersTheShow();
            }
        }

        if (releasedLegs > 3)
        {
            movement.speed = 0.15f;
        }
    }

    private void CameraChange()
    {
        if (!cameraChanged)
        {
            /* TIMELINE*/

            //Esto desactiva la camara inicial lo que inicia la transicion inicial, te devuelve los poderes con 
            //powersBack (esto debería llamarse en la timeline cuando la transición acabe)

            //Te devuelve los poderes
            cameraChanged = true;
            windDown = true;
            StartingCamera.SetActive(false);
            StartCoroutine("powersBack");
        }

    }


    IEnumerator powersBack()
    {
        yield return new WaitForSeconds(8f);
        windDown = false;
        currentLerpTime = 0;
        mouse.SetActive(true);

    }

    private void SurkaEntersTheShow()
    {
        if (!surkaSpawned)
        {
            surkaSpawned = true;

            /* TIMELINE*/
            timeline_interface.TPlay();
        }
    }
}
