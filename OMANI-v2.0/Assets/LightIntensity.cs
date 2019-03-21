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
    bool firstTimePassed, justPassed1, justPassed2 = true;

    // Update is called once per frame
    void Update()
    {

        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (firstTimePassed)
        {
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
                directional.intensity = Mathf.Lerp(directional.intensity, 1.2f, t);
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

                directional.intensity = Mathf.Lerp(directional.intensity, 0.75f, t);
                wind.volume = Mathf.Lerp(wind.volume, normalVolume, t);
            }
        }


        else
        {
            if (playerDistance < 90f) { firstTimePassed = true; }
        }
    }

    void resetCurrentLerpTime()
    {
        currentLerpTime = 0;
    }
}
