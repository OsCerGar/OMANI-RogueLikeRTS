using System.Collections;
using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public CharacterMovement movement;
    public Powers powers;
    public OMANINPUT controls;

    [SerializeField] GameObject StartingCamera, SurkaCamera;

    [SerializeField] private InverseKinematics leg1, leg2, leg3, leg4;
    bool cameraChanged, surkaSpawned;


    //SURKA
    [SerializeField] Animator surkaAnim;

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

        StartCoroutine("surkaHittingBag");

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SurkaEntersTheShow(); movement.speed = 0.15f;
        }
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
        yield return new WaitForSeconds(5f);
        powers.enabled = true;
    }
    IEnumerator surkaHittingBag()
    {
        yield return new WaitForSeconds(5f);
        SurkaHittingBag();
    }

    IEnumerator surkaRoutine()
    {
        yield return new WaitForSeconds(60f);
        SurkaEntersTheShow();
    }

    private void SurkaHittingBag()
    {
        if (!surkaSpawned)
        {
            surkaAnim.SetTrigger("Attack");
            StartCoroutine("surkaHittingBag");
        }
    }

    private void SurkaEntersTheShow()
    {
        surkaSpawned = true;
        SurkaCamera.SetActive(true);

        surkaAnim.SetTrigger("StartRunning");
        surkaAnim.SetFloat("Z", 1f);

        StartCoroutine("surkaCameraRoutine");
    }

    IEnumerator surkaCameraRoutine()
    {
        yield return new WaitForSeconds(6f);
        SurkaCamera.SetActive(false);
    }
}
