using UnityEngine;

public class Powers : MonoBehaviour
{
    [SerializeField]
    int ennuisMask;
    PW_SlowMotion slowMo;
    PW_Hearthstone hearthStone;
    LookDirectionsAndOrder lookDirection;
    PowerManager powerManager;
    PW_Dash dash;
    Power_Laser lasers;

    [SerializeField]
    public float maxpowerPool = 1000, powerPool = 1000, increaseAmount = 1, bigLazerAmount = 20, smallLazerAmount = 1, laserCooldown = 1, laserTime;
    int quarter, half, quartandhalf;

    float radius = 3;

    private void Awake()
    {
        Initializer();
    }
    void Initializer()
    {
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
        slowMo = transform.GetComponent<PW_SlowMotion>();
        hearthStone = transform.GetComponent<PW_Hearthstone>();
        lookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        powerManager = FindObjectOfType<PowerManager>();
        dash = FindObjectOfType<PW_Dash>();
        lasers = FindObjectOfType<Power_Laser>();

        quarter = Mathf.RoundToInt(maxpowerPool * 0.25f);
        half = Mathf.RoundToInt(maxpowerPool * 0.5f);
        quartandhalf = Mathf.RoundToInt(maxpowerPool * 0.75f);

    }
    // Update is called once per frame
    void Update()
    {
        #region Inputs
        #region LaserBeams

        // /3 because the limit size of the sphere is 0.33.
        lasers.setSphereWidth((powerPool / maxpowerPool) / 3);

        if (Input.GetKey("joystick button 6"))
        {
            //Attack mode
            if (Input.GetKeyDown("joystick button 7"))
            {
                if (Time.time - laserTime > laserCooldown && reducePowerNow(3))
                {   //Attack Beam
                    lasers.EmitOffensiveLaser();
                    laserTime = Time.time;
                }
            }
        }
        else
        {        //Controller
            if (Input.GetKeyDown("joystick button 7"))
            {
                lasers.StartEffects();
            }
            if (Input.GetKey("joystick button 7"))
            {

                //Energy Beam

                lasers.EmitLaser();
            }
        }



        //Attack mode
        if (Input.GetKey(KeyCode.LeftShift))
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                if (Time.time - laserTime > laserCooldown && reducePowerNow(3))
                {   //Attack Beam
                    lasers.EmitOffensiveLaser();
                    laserTime = Time.time;

                }

            }
        }
        else
        {
            //Kb&M
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                lasers.StartEffects();
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {

                //Energy Beam
                lasers.EmitLaser();
            }
        }
        #endregion
        #region SlowMotion
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 3"))
        {
            slowMo.CastPower();
        }
        #endregion
        #region Hearthstone
        if (Input.GetKey(KeyCode.Q) || Input.GetKey("joystick button 0"))
        {
            hearthStone.CastPower();
        }
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp("joystick button 0"))
        {
            hearthStone.StopCast();
        }

        #endregion
        #region Dash

        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 1"))
        {
            dash.CastPower();
        }

        else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("joystick button 1"))
        {
            dash.StopRunning();
        }

        #endregion

        #endregion

        #region IncreasePowerPool
        if (powerPool < quarter)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, quarter);
        }
        else if (powerPool < half)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, half);
        }
        else if (powerPool < quartandhalf)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, quartandhalf);
        }
        else if (powerPool < maxpowerPool)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, maxpowerPool);
        }
        #endregion
    }
    public void addPower(float amount)
    {
        powerPool = Mathf.Clamp(powerPool + amount, 0, maxpowerPool);
    }
    public bool reducePower(float amount)
    {
        float finalAmount = amount * Time.unscaledDeltaTime;
        if (powerPool - finalAmount >= 0)
        {
            powerPool -= finalAmount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool reducePowerNow(float amount)
    {
        if (powerPool - amount >= 0)
        {
            powerPool -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public float reduceAsMuchPower(float amount)
    {
        float energyReduced;
        if (powerPool - amount >= 0)
        {
            powerPool -= amount;
            energyReduced = 10000;
        }
        else
        {
            energyReduced = powerPool;

            powerPool -= powerPool;
        }
        return energyReduced;
    }

    private void FixedUpdate()
    {
        FindEnnuis();
    }

    private void FindEnnuis()
    {
        if (powerPool < maxpowerPool)
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, radius, ennuisMask);
            foreach (Collider col in targetsInViewRadius)
            {
                if (col.tag == "Ennui")
                {
                    // Save the col as an NPC
                    Ennui_Ground ennui;
                    ennui = col.GetComponent<Ennui_Ground>();

                    if (ennui != null)
                    {
                        ennui.Action(this);
                    }
                }
            }
        }
    }
}
