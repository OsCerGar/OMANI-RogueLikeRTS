using System.Collections;
using UnityEngine;

public class TutorialSurkaTeleport : MonoBehaviour
{
    [SerializeField] Animator surkaAnim;
    [SerializeField] GameObject SurkaCamera;
    bool cameraChanged;
    [SerializeField] GameObject cinemaMode;
    public OMANINPUT controls;
    public CharacterMovement movement;

    public SpriteRenderer MouseSprite, MouseSprite2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cameraChanged)
        {
            cameraChanged = true;
            //DisableMovementAndPowers
            surkaAnim.SetTrigger("AnimationTutorial2");
            surkaAnim.SetFloat("Z", 1f);
            SurkaCamera.SetActive(true);
            cinemaMode.SetActive(true);
            DisableControls();
            StartCoroutine("surkaCameraRoutine");
        }

    }

    IEnumerator surkaCameraRoutine()
    {
        yield return new WaitForSeconds(6f);
        SurkaCamera.SetActive(false);
        StartCoroutine("returnControls");

    }

    IEnumerator returnControls()
    {
        yield return new WaitForSeconds(5f);
        cinemaMode.SetActive(false);
        EnableControls();
    }


    private void EnableControls()
    {
        movement.speed = 0.15f;
        MouseSprite.enabled = true;
        MouseSprite2.enabled = true;

        controls.PLAYER.WASD.Enable();
        controls.PLAYER.Joystick.Enable();
        controls.PLAYER.LASERZONE.Enable();
        controls.PLAYER.LASERZONERELEASE.Enable();
        controls.PLAYER.LASERSTRONGPREPARATION.Enable();
        controls.PLAYER.LASERSTRONG.Enable();
    }
    private void DisableControls()
    {
        movement.speed = 0;

        MouseSprite.enabled = false;
        MouseSprite2.enabled = false;

        controls.PLAYER.WASD.Disable();
        controls.PLAYER.Joystick.Disable();
        controls.PLAYER.LASERZONE.Disable();
        controls.PLAYER.LASERZONERELEASE.Disable();
        controls.PLAYER.LASERSTRONGPREPARATION.Disable();
        controls.PLAYER.LASERSTRONG.Disable();
    }


}
