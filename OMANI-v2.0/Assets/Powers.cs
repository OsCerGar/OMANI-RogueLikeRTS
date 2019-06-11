using UnityEngine;
public class Powers : MonoBehaviour
{
    #region ReferenceVariables
    int ennuisMask;
    PW_Hearthstone hearthStone;
    LookDirectionsAndOrder lookDirection;
    PowerManager powerManager;
    PW_Dash dash;
    Power_Laser lasers;
    public Army army;
    #endregion

    [SerializeField]
    public float maxArmor = 1000, armor = 1000, increaseAmount = 1, bigLazerAmount = 20, smallLazerAmount = 1, laserCooldown = 1, laserTime;
    //Test
    public Vector3 MiradaPosition;
    float radius = 2;

    //INPUTS
    public bool zonelaservalue = false, stronglaservalue = false, hearthStoneValue = false;


    public bool connected;
    public Transform connectObject;

    #region controller variables
    private bool pressed;

    #endregion
    #region Initializers

    //input
    PlayerInputInterface player;
    private void Awake()
    {
        Initializer();
        //controls.PLAYER.HEARTHSTONE.performed += context => HearthstoneValue();
    }
    void Initializer()
    {
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
        hearthStone = transform.GetComponent<PW_Hearthstone>();
        lookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        army = FindObjectOfType<Army>();
        powerManager = FindObjectOfType<PowerManager>();
        player = FindObjectOfType<PlayerInputInterface>();
        dash = FindObjectOfType<PW_Dash>();
        lasers = FindObjectOfType<Power_Laser>();

    }
    #endregion

    public void Start()
    {

        maxArmor = float.Parse(GamemasterController.GameMaster.getCsvValues("Omani")[1]);

    }
    public void ConnectedValue(bool _connectedValue, Transform _connectedObject)
    {
        connected = _connectedValue;
        connectObject = _connectedObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (army.currentFighter == null)
        {
            #region Inputs 
            if (player.Laser) { if (!pressed) { lasers.StartEffects(); pressed = true; } }
            if (player.Laser) { ZoneLaser(); }
            if (!player.Laser)
            {
                if (pressed)
                {
                    lasers.EmitLaserStop();
                    ConnectedValue(false, null);
                    pressed = false;
                }
            }

            #endregion
        }
        else
        {
            lasers.StopParticles();
            pressed = false;
        }
        #region LaserBeams

        // /3 because the limit size of the sphere is 0.33.
        lasers.setSphereWidth((armor / maxArmor) / 2);

        //ds4light
        if (player.ds4 != null)
        {
            player.SetDS4Lights(new Color(0, 0.75f, 0.0f, (armor / maxArmor)));
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


    //for the future
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
        armor = Mathf.Clamp(armor + amount, 0, maxArmor);
    }
    public float addPowerReturn(float amount)
    {
        float excessArmor = armor + amount - maxArmor;
        armor = Mathf.Clamp(armor + amount, 0, maxArmor);

        if (excessArmor > 0)
        {
            return excessArmor;
        }
        else
        {
            return amount;
        }

    }
    public bool restorePower(float amount)
    {
        float finalAmount = amount * Time.unscaledDeltaTime;
        if (armor + finalAmount < maxArmor)
        {
            armor += finalAmount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool reducePower(float amount)
    {
        float finalAmount = amount * Time.unscaledDeltaTime;
        if (armor - finalAmount >= 0)
        {
            armor -= finalAmount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool reducePowerNow(float amount)
    {
        if (armor - amount >= 0)
        {
            armor -= amount;
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
        if (armor - amount >= 0)
        {
            armor -= amount;
            energyReduced = amount;
        }
        else
        {
            energyReduced = armor;
            armor -= armor;
        }
        return energyReduced;
    }
    #endregion

    #region EnnuiRelated
    private void FindEnnuis()
    {
        if (armor < maxArmor)
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, radius, ennuisMask);
            foreach (Collider col in targetsInViewRadius)
            {
                if (col.tag == "Ennui")
                {
                    Debug.Log("Catched");
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
