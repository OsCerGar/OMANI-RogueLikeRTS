using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using Cinemachine.PostFX;

public class PW_SlowMotion : Power
{

    private float slowdownFactor = 0.25f;
    private float slowdownLength = 1.5f, recovery = 5f, waste = 10f, viewRadius = 2f, regularSpeed;
    private bool active = false;
    private int targetMask = 1 << 10;

    [SerializeField]
    PostProcessingProfile slowmo;
    PostProcessingProfile normal;
    CinemachinePostFX postFx;
    CharacterMovement player;
    Animator animatorSpeed;
    List<NPC> hittedNpcs = new List<NPC>();

    private void Start()
    {
        maxpowerPool = 100;
        powerPool = 100;

        postFx = FindObjectOfType<CinemachinePostFX>();
        player = GetComponent<CharacterMovement>();
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
            powerPool = Mathf.Clamp(powerPool + (Time.unscaledDeltaTime * recovery), 0, maxpowerPool);

            // turns speed back
            player.speed = regularSpeed;
        }
        else
        {
            powerPool = Mathf.Clamp(powerPool - (Time.unscaledDeltaTime * waste), 0, maxpowerPool);

            // Player goes faster
            player.speed = 0.1f;

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
            foreach (Collider col in targetsInViewRadius)
            {
                NPC enemy = col.transform.root.GetComponent<NPC>();
                bool found = false;
                foreach (NPC _npc in hittedNpcs)
                {
                    if (_npc == enemy)
                    {
                        found = true;
                    }
                }
                if (enemy != null && found == false)
                {
                    hittedNpcs.Add(enemy);
                    enemy.TakeDamage(50, true, 1f, transform);
                }
            }
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
