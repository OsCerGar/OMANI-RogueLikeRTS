using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource combatMusic, combatStop, mainTheme;
    bool isPlaying;
    float timeToStop;

    float lerpTime = 12f;
    float currentLerpTime;

    public static MusicManager musicManager;
    //Room closed
    public void roomClosed()
    {
        timeToStop = Time.time;
        StartCoroutine("stopMusic");

    }

    //room open
    public void roomStart()
    {
        if (!isPlaying)
        {
            currentLerpTime = 0;
            combatMusic.Play(); mainTheme.volume = 0;
            isPlaying = true;
        }
    }

    private void Start()
    {
        if (musicManager == null)
        {
            musicManager = this;
        }
    }

    void Update()
    {
        //stops after 30 seconds
        if (Time.time - timeToStop > 30f)
        {
            timeToStop = 999;
            StopCombatMusic();
        }

        if (isPlaying)
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            combatMusic.volume = 1f;
            mainTheme.volume = Mathf.Lerp(mainTheme.volume, 0f, t);
        }
        else
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            combatMusic.volume = 0f;
            mainTheme.volume = Mathf.Lerp(mainTheme.volume, 1f, t);

            if (combatMusic.isPlaying && combatMusic.volume <= 0.1f)
            {
                combatMusic.Stop();
                combatStop.Play();
                currentLerpTime = 0f;
            }
        }
    }

    private void StopCombatMusic()
    {
        isPlaying = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        combatMusic = transform.Find("CombatMusic").GetComponent<AudioSource>();
        combatStop = transform.Find("CombatMusicStop").GetComponent<AudioSource>();
        mainTheme = transform.Find("MainTheme").GetComponent<AudioSource>();
    }

    private IEnumerator stopMusic()
    {
        yield return new WaitForSeconds(4.5f);
        StopCombatMusic();

    }

}
