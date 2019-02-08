using System.Collections;
using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public CharacterMovement movement;
    public Powers powers;
    public OMANINPUT controls;

    [SerializeField] GameObject StartingCamera, SurkaCamera;
    [SerializeField] GameObject SneakySurka;

    [SerializeField] private InverseKinematics leg1, leg2, leg3, leg4;
    bool cameraChanged, surkaSpawned;


    private void Awake()
    {
        controls.PLAYER.WASD.performed += movement => CameraChange();
        controls.PLAYER.Joystick.performed += Controllermovement => CameraChange();
        controls.PLAYER.LASERZONE.performed += context => CameraChange();
        controls.PLAYER.LASERZONERELEASE.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONGPREPARATION.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONG.performed += context => CameraChange();
        controls.PLAYER.RadialMenuUp.Disable();
        controls.PLAYER.RadialMenuDown.Disable();

    }


    // Start is called before the first frame update
    void Start()
    {
        StartRestrictions();
    }

    private void StartRestrictions()
    {
        movement.speed = 0;
        powers.enabled = false;

        controls.PLAYER.Joystick.Enable();
        controls.PLAYER.LASERZONE.Enable();
        controls.PLAYER.LASERZONERELEASE.Enable();
        controls.PLAYER.LASERSTRONGPREPARATION.Enable();
        controls.PLAYER.LASERSTRONG.Enable();
        controls.PLAYER.RadialMenuUp.Enable();
        controls.PLAYER.RadialMenuDown.Enable();

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
        if (!surkaSpawned)
        {
            if (leg1.enabled && leg2.enabled && leg3.enabled)
            {
                surkaSpawned = true;
                SurkaEntersTheShow();
            }
        }

        if (leg1.enabled && leg2.enabled && leg3.enabled && leg4.enabled)
        {
            movement.speed = 0.15f;
        }
    }

    private void CameraChange()
    {
        if (!cameraChanged)
        {
            StartingCamera.SetActive(false);
            StartCoroutine("surkaRoutine");
            StartCoroutine("powersBack");
            cameraChanged = true;
        }

    }

    IEnumerator powersBack()
    {
        yield return new WaitForSeconds(2f);
        powers.enabled = true;
    }

    IEnumerator surkaRoutine()
    {
        yield return new WaitForSeconds(20f);
        SurkaEntersTheShow();
    }

    private void SurkaEntersTheShow()
    {
        if (!surkaSpawned)
        {
            SneakySurka.SetActive(true);
            SurkaCamera.SetActive(true);
            StartCoroutine("surkaCameraRoutine");
            surkaSpawned = true;
        }
    }

    IEnumerator surkaCameraRoutine()
    {
        yield return new WaitForSeconds(6f);
        SurkaCamera.SetActive(false);
    }
}
