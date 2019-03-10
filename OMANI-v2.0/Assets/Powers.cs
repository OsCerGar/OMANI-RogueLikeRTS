using System.Collections;
using UnityEngine;

public class Powers : MonoBehaviour
{
    #region ReferenceVariables
    [SerializeField]
    int ennuisMask;
    PW_Hearthstone hearthStone;
    LookDirectionsAndOrder lookDirection;
    PowerManager powerManager;
    PW_Dash dash;
    Power_Laser lasers;
    Army army;
    #endregion

    [SerializeField]
    public float maxpowerPool = 1000, powerPool = 1000, increaseAmount = 1, bigLazerAmount = 20, smallLazerAmount = 1, laserCooldown = 1, laserTime;
    //Test
    public Vector3 MiradaPosition;
    int quarter, half, quartandhalf;

    float radius = 3;

    //INPUTS
    public OMANINPUT controls;
    public bool zonelaservalue = false, stronglaservalue = false, hearthStoneValue = false;


    public bool connected;
    public Transform connectObject;

    #region Initializers
    private void Awake()
    {
        Initializer();

        controls.PLAYER.OrderLaser.performed += context => ZoneLaserValue();
        controls.PLAYER.HEARTHSTONE.performed += context => HearthstoneValue();
    }
    void Initializer()
    {
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
        hearthStone = transform.GetComponent<PW_Hearthstone>();
        lookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        army = FindObjectOfType<Army>();
        powerManager = FindObjectOfType<PowerManager>();
        dash = FindObjectOfType<PW_Dash>();
        lasers = FindObjectOfType<Power_Laser>();
        quarter = Mathf.RoundToInt(maxpowerPool * 0.25f);
        half = Mathf.RoundToInt(maxpowerPool * 0.5f);
        quartandhalf = Mathf.RoundToInt(maxpowerPool * 0.75f);

    }
    #endregion
    #region Events
    private void OnEnable()
    {
        enable();
    }
    private void OnDisable()
    {
        disable();
    }
    #endregion

    public void Start()
    {
        StartCoroutine("restartControls");
    }

    IEnumerator restartControls()
    {
        //maybethisworks
        disable();

        yield return new WaitForSeconds(1f);
        enable();

    }

    public void ConnectedValue(bool _connectedValue, Transform _connectedObject)
    {
        connected = _connectedValue;
        connectObject = _connectedObject;
    }

    private void enable()
    {
        controls.PLAYER.OrderLaser.Enable();
        controls.PLAYER.LASERZONERELEASE.Enable();
        controls.PLAYER.HEARTHSTONE.Enable();
    }
    private void disable()
    {
        controls.PLAYER.OrderLaser.Disable();
        controls.PLAYER.LASERZONERELEASE.Disable();
        controls.PLAYER.HEARTHSTONE.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        #region LaserBeams

        // /3 because the limit size of the sphere is 0.33.
        lasers.setSphereWidth((powerPool / maxpowerPool) / 3);

        //ZoneLaser
        if (zonelaservalue) { ZoneLaser(); }

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
    private void FixedUpdate()
    {
        FindEnnuis();
    }

    #region InputRelated
    private void ZoneLaser()
    {
        lasers.EmitLaser(connected, connectObject);
    }

    private void ZoneLaserValue()
    {
        if (army.currentFighter == null)
        {
            if (zonelaservalue)
            {
                zonelaservalue = false;
                ConnectedValue(false, null);
                lasers.EmitLaserStop();
            }
            else
            {
                zonelaservalue = true;
                lasers.StartEffects();
            }
        }
    }

    private void StrongLaserValue()
    {
        if (stronglaservalue)
        {
            stronglaservalue = false;

        }
        else
        {
            stronglaservalue = true;

            lasers.StartEffects(); //Strong laser preparation effects
        }
    }
    private void StrongLaser()
    {
        if (stronglaservalue)
        {
            if (Time.time - laserTime > laserCooldown && reducePowerNow(3))
            {   //Attack Beam
                lasers.EmitOffensiveLaser();
                laserTime = Time.time;
            }
        }
    }

    private void HearthstoneValue()
    {
        if (hearthStoneValue)
        {
            hearthStoneValue = false;
            hearthStone.StopCast();

        }
        else
        {
            hearthStoneValue = true;

            hearthStone.CastPower();
        }
    }

    #endregion

    #region PowerRelated
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
            energyReduced = amount;
        }
        else
        {
            energyReduced = powerPool;
            powerPool -= powerPool;
        }
        return energyReduced;
    }
    #endregion

    #region EnnuiRelated
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
    #endregion
}
