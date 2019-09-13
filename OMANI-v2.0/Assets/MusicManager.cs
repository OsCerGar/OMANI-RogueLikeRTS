using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource combatMusic, combatStop, mainTheme, ambiente;
    float combatMusicVolume, combatStopVolume, mainThemeVolume, ambienteVolume, ambienteVolume2 = 0.7f;
    bool isPlaying;
    float timeToStop;

    float lerpTime = 12f;
    float currentLerpTime;

    public static MusicManager musicManager;
    //Room closed
    public void roomClosed()
    {

    }

    //room open
    public void roomStart()
    {

    }

    private void StopCombatMusic()
    {
    }
    public void LowerMusic(string theme, float time)
    {
        if (theme == "mainTheme")
        {
            StartCoroutine(FadeOut(mainTheme, time, 0f));
        }
        if (theme == "combatMusic")
        {
            Debug.Log("Lowering combatMusic");
            StartCoroutine(FadeOut(combatMusic, 1f, 0f));
        }
        if (theme == "combatStop")
        {

            StartCoroutine(FadeOut(combatStop, 1f, 0f));
        }
        if (theme == "ambiente")
        {

            StartCoroutine(FadeOut(ambiente, 1f, ambienteVolume));
        }
    }
    public void MusicUp(string theme)
    {
        if (theme == "mainTheme")
        {
            StartCoroutine(FadeIn(mainTheme, 6f));
        }
        if (theme == "combatMusic")
        {
            combatMusic.Play();
            StartCoroutine(FadeIn(combatMusic, 2f));
        }
        if (theme == "combatStop")
        {
            combatStop.Play();

            StartCoroutine(FadeIn(combatStop, 1f));
        }
        if (theme == "ambiente")
        {

            StartCoroutine(FadeIn(ambiente, 1f));
        }
    }
    public IEnumerator FadeOut(AudioSource theme, float time, float finalVolume)
    {
        yield return new WaitForSeconds(0.01f);

        if (theme.volume > finalVolume)
        {
            theme.volume -= Time.deltaTime / time;
            StartCoroutine(FadeOut(theme, time, finalVolume));
        }
        else if (theme.volume <= 0)
        {
            if (theme == combatMusic)
            {
                combatMusic.Stop();
            }
            else if (theme == combatStop)
            {
                combatStop.Stop();
            }
        }
    }


    public IEnumerator FadeIn(AudioSource theme, float time)
    {
        yield return new WaitForSeconds(0.01f);

        if (theme == mainTheme)
        {
            if (theme.volume < mainThemeVolume)
            {
                theme.volume += Time.deltaTime / time;
                StartCoroutine(FadeIn(theme, time));
            }
        }
        else if (theme == combatMusic)
        {
            if (theme.volume < combatMusicVolume)
            {
                theme.volume += combatMusicVolume;
                StartCoroutine(FadeIn(theme, time));
            }
        }
        else if (theme == combatStop)
        {
            if (theme.volume < combatStopVolume)
            {
                theme.volume += combatStopVolume;
                StartCoroutine(FadeIn(theme, time));
            }
        }
        else if (theme == ambiente)
        {
            if (theme.volume < ambienteVolume2)
            {
                theme.volume += ambienteVolume2;
                StartCoroutine(FadeIn(theme, time));
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (musicManager == null)
        {
            musicManager = this;
        }
        combatMusic = transform.Find("CombatMusic").GetComponent<AudioSource>();
        combatMusicVolume = combatMusic.volume;
        combatStop = transform.Find("CombatMusicStop").GetComponent<AudioSource>();
        combatStopVolume = combatStop.volume;

        mainTheme = transform.Find("MainTheme").GetComponent<AudioSource>();
        mainThemeVolume = mainTheme.volume;
        ambiente = transform.Find("Viento").GetComponent<AudioSource>();
        ambienteVolume = ambiente.volume;

    }

    private IEnumerator stopMusic()
    {
        yield return new WaitForSeconds(4.5f);
        StopCombatMusic();

    }

}
