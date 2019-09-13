using System.Collections;
using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public CharacterMovement movement;

    int releasedLegs = 0;
    bool cameraChanged, surkaSpawned;
    TIMELINE_INTERFACE timeline_interface;

    [SerializeField] GameObject StartingCamera, mouse, disablePlayer, doorLock;
    //Wind sound effect
    [SerializeField]
    AudioSource wind, music;
    [SerializeField]
    float finalVolume;
    float currentLerpTime, lerpTime = 1f;
    bool windDown, disabling;

    [SerializeField]
    GameObject tutorials;

    [SerializeField]
    GameObject TextIntro, TextSurka, TextSurkaEscapes;

    private void Start()
    {
        timeline_interface = GetComponent<TIMELINE_INTERFACE>();
        tutorials = transform.Find("Tutorials").gameObject;
        disablePlayer.SetActive(true);
        movement.anim.SetTrigger("LayDown");
    }


    private void Update()
    {
        movement.StopMovement();

        if (Input.anyKeyDown) { CameraChange(); }

        if (windDown)
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.0025f);

            wind.volume = Mathf.Lerp(wind.volume, finalVolume, t);
        }
        if (disabling)
        {
            if (movement.anim.GetCurrentAnimatorStateInfo(0).IsName("ANIM_IDDLE_4_patas"))
            {
                movement.AbleToMove();
                enabled = false;
            }
        }

    }


    public void LegRelease(int _leg)
    {

        releasedLegs++;

        if (!surkaSpawned)
        {
            if (releasedLegs > 0) { tutorials.SetActive(false); }
            if (releasedLegs > 1) { TextSurka.SetActive(true); disablePlayer.SetActive(true); }
            if (releasedLegs > 2)
            {
                SurkaEntersTheShow();
            }
        }

        if (releasedLegs > 3)
        {
            doorLock.SetActive(false);
            movement.anim.SetTrigger("Free");
            disabling = true;
            TextSurkaEscapes.SetActive(true);


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

            music.Play();
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
        TextIntro.SetActive(true);

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
