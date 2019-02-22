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

    [SerializeField] Animator teleport;

    TIMELINE_INTERFACE timeline_interface;

    private void Awake()
    {
        timeline_interface = GetComponent<TIMELINE_INTERFACE>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cameraChanged)
        {
            cameraChanged = true;


            //DisableMovementAndPowers

            //Timelineshit
            timeline_interface.TPlay();
            timeline_interface.DisableControls();
            cinemaMode.SetActive(true);
        }

    }
    IEnumerator teleportRoutine()
    {
        yield return new WaitForSeconds(3f);
        teleport.SetTrigger("Activated");
    }
}
