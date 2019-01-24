using UnityEngine;
//using UnityEngine.PostProcessing;

public class PW_SlowMotion : Power
{

    private float slowdownFactor = 0.25f;
    private float slowdownLength = 1.5f, waste = 10f, viewRadius = 2f, regularSpeed, energyCost = 15;
    private bool active = false, backToNormal = false;
    private int targetMask = 1 << 10;

    [SerializeField]
    //PostProcessingProfile slowmo;
    //PostProcessingProfile normal;

    ConfigurableJoint queenJoint;

    public override void Awake()
    {
        base.Awake();

        queenJoint = FindObjectOfType<ConfigurableJoint>();
        regularSpeed = player.speed;
    }

    public override void Update()
    {
        if (!active)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        }
        else
        {
            if (!powers.reducePower(energyCost))
            {
                active = false;
            }
        }
    }

    private void SlowMotion()
    {
        if (!active)
        {
            queenJoint.zMotion = ConfigurableJointMotion.Locked;
            queenJoint.angularXMotion = ConfigurableJointMotion.Locked;
            queenJoint.angularYMotion = ConfigurableJointMotion.Locked;
            queenJoint.angularZMotion = ConfigurableJointMotion.Locked;

            // if inactive becomes active and loads the slowmo postfx added in inspector.
            backToNormal = false;
            active = true;
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            // Player goes faster
             player.speed = 0.05f;

            locomotionBrain.SlowMotionValues();
        }
        else
        {
            queenJoint.zMotion = ConfigurableJointMotion.Limited;
            queenJoint.angularXMotion = ConfigurableJointMotion.Limited;
            queenJoint.angularYMotion = ConfigurableJointMotion.Limited;
            queenJoint.angularZMotion = ConfigurableJointMotion.Limited;

            // if already active becomes inactive and loads the regular postfx.
            active = false;
            Time.fixedDeltaTime = 0.02F;

            // turns speed back
            player.speed = regularSpeed;

            locomotionBrain.StopSlowMotionValues();
        }
    }

    public override void CastPower()
    {
        SlowMotion();
    }
}
