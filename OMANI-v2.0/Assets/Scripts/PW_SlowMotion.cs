using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Cinemachine.PostFX;

public class PW_SlowMotion : Power
{

    private float slowdownFactor = 0.25f;
    private float slowdownLength = 1.5f, recovery = 5f, waste = 10f;
    private bool active = false;

    [SerializeField]
    PostProcessingProfile slowmo;
    PostProcessingProfile normal;
    CinemachinePostFX postFx;

    private void Start()
    {
        Debug.Log("STart" + Time.timeScale);
        maxpowerPool = 100;
        powerPool = 100;

        postFx = FindObjectOfType<CinemachinePostFX>();
        normal = postFx.m_Profile;
    }

    public void Update()
    {
        Debug.Log(Time.timeScale);
        if (active == false)
        {
            postFx.m_Profile = normal;
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            powerPool = Mathf.Clamp(powerPool + (Time.unscaledDeltaTime * recovery), 0, maxpowerPool);
        }
        else
        {
            powerPool = Mathf.Clamp(powerPool - (Time.unscaledDeltaTime * waste), 0, maxpowerPool);
        }

        if (powerPool < 1)
        {
            active = false;
        }
    }


    public void SlowMotion()
    {
        if (active == false)
        {
            // if inactive becomes active and loads the slowmo postfx added in inspector.
            active = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.2f;
            postFx.m_Profile = slowmo;
        }
        else
        {
            // if already active becomes inactive and loads the regular postfx.
            active = false;
            postFx.m_Profile = normal;

        }
    }

    public override void CastPower()
    {
        SlowMotion();
    }
}
