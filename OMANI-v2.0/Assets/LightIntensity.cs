using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Light directional;

    float playerDistance;
    float t = 0;

    //Wind sound effect
    [SerializeField]
    AudioSource wind;
    [SerializeField]
    float maxVolume, normalVolume;

    float currentLerpTime, lerpTime = 0.25f;
    bool justPassed1, justPassed2  = true;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < 35f)
        {
            if (!justPassed1)
            {
                resetCurrentLerpTime();
                justPassed1 = true;
                justPassed2 = false;
            }

            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.0025f);

            //t = Mathf.Sin(t * Mathf.PI * 0.0025f);
            directional.intensity = Mathf.Lerp(directional.intensity, 1f, t);
            wind.volume = Mathf.Lerp(wind.volume, maxVolume, t);

        }
        else
        {
            if (!justPassed2)
            {
                resetCurrentLerpTime();
                justPassed2 = true;
                justPassed1 = false;
            }

            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.0025f);

            directional.intensity = Mathf.Lerp(directional.intensity, 0.5f, t);
            wind.volume = Mathf.Lerp(wind.volume, normalVolume, t);
        }
    }

    void resetCurrentLerpTime()
    {
        Debug.Log(playerDistance);
        currentLerpTime = 0;
    }
}
