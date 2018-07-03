using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Cinemachine.PostFX;

public class PW_SlowMotion : Power
{

    private float slowdownFactor = 0.25f;
    private float slowdownLength = 1.5f, waste = 10f, viewRadius = 2f, regularSpeed, powerToReduce;
    private bool active = false;
    private int targetMask = 1 << 10;

    [SerializeField]
    PostProcessingProfile slowmo;
    PostProcessingProfile normal;
    CinemachinePostFX postFx;
    CharacterMovement player;
    Animator animatorSpeed;
    Powers powers;

    List<NPC> hittedNpcs = new List<NPC>();

    private void Start()
    {
        postFx = FindObjectOfType<CinemachinePostFX>();
        player = GetComponent<CharacterMovement>();
        powers = FindObjectOfType<Powers>();
        normal = postFx.m_Profile;
        regularSpeed = player.speed;
    }

    public void Update()
    {
        if (active == false)
        {
            postFx.m_Profile = normal;
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = 0.02F;

            // turns speed back
            player.speed = regularSpeed;
        }
        else
        {
            if (powerToReduce < 1)
            {
                powerToReduce += Time.unscaledDeltaTime * waste;
            }
            else
            {
                powerToReduce = 0;
                if (!powers.reducePower(1))
                {
                    active = false;
                }
            }
            // Player goes faster
            player.speed = 0.1f;
        }
    }

    private void SlowMotion()
    {
        if (active == false)
        {
            // if inactive becomes active and loads the slowmo postfx added in inspector.
            active = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            postFx.m_Profile = slowmo;
        }
        else
        {
            // cleans list
            hittedNpcs.Clear();
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
