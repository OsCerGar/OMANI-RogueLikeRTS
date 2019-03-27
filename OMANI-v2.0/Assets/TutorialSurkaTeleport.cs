using System.Collections;
using UnityEngine;

public class TutorialSurkaTeleport : MonoBehaviour
{
    [SerializeField] Animator surkaAnim;
    [SerializeField] GameObject SurkaCamera;
    bool cameraChanged;
    public CharacterMovement movement;

    public SpriteRenderer MouseSprite, MouseSprite2;

    [SerializeField] Animator teleport;

    TIMELINE_INTERFACE timeline_interface;

    [SerializeField] GameObject MovementTutorial;

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
            MovementTutorial.SetActive(false);
            timeline_interface.TPlay();
        }

    }
    IEnumerator teleportRoutine()
    {
        yield return new WaitForSeconds(3f);
        teleport.SetTrigger("Activated");
    }
}
