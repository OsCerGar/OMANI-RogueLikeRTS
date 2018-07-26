using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    [SerializeField]
    public List<Power> power = new List<Power>();
    int ennuisMask;
    PW_SlowMotion slowMo;
    PW_Hearthstone hearthStone;
    LookDirectionsAndOrder lookDirection;
    PowerManager powerManager;
    PW_Dash dash;

    [SerializeField]
    public float maxpowerPool = 100, powerPool = 100, increaseAmount = 1, bigLazerAmount = 20, smallLazerAmount = 1;

    float radius = 3;

    private void Awake()
    {
        Initializer();
    }

    void Initializer()
    {
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
        slowMo = this.transform.GetComponent<PW_SlowMotion>();
        hearthStone = this.transform.GetComponent<PW_Hearthstone>();
        lookDirection = FindObjectOfType<LookDirectionsAndOrder>();
        powerManager = FindObjectOfType<PowerManager>();
        dash = FindObjectOfType<PW_Dash>();

    }

    // Update is called once per frame
    void Update()
    {
        #region Inputs
        #region LaserBeams
        //Controller
        if (Input.GetKeyDown("joystick button 7"))
        {
            if (this.reducePower(smallLazerAmount))
            {
                //Energy Beam
                powerManager.ShootBasicPower(lookDirection.transform);
            }
        }

        if (Input.GetKey("joystick button 6"))
        {
            //Attack mode

            if (Input.GetKeyDown("joystick button 7"))
            {
                if (this.reducePower(bigLazerAmount))
                {
                    //Attack Beam
                    powerManager.ShootUpgradedPower(lookDirection.transform);
                }
            }
        }

        //Kb&M
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Attack mode

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (this.reducePower(smallLazerAmount))
                {
                    //Energy Beam
                    powerManager.ShootBasicPower(lookDirection.transform);
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (this.reducePower(bigLazerAmount))
                {
                    //Attack Beam
                    powerManager.ShootUpgradedPower(lookDirection.transform);
                }
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
        if (powerPool <= maxpowerPool * 0.25f)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, maxpowerPool * 0.25f);
        }
        else if (powerPool <= maxpowerPool * 0.5f)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, maxpowerPool * 0.5f);
        }
        else if (powerPool <= maxpowerPool * 0.75f)
        {
            powerPool = Mathf.Clamp(powerPool + increaseAmount * Time.unscaledDeltaTime, 0, maxpowerPool * 0.75f);
        }
        else
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
        if (powerPool - amount >= 0)
        {
            powerPool -= amount * Time.unscaledDeltaTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        FindEnnuis();
    }

    private void FindEnnuis()
    {
        if (powerPool < maxpowerPool)
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(this.transform.position, radius, ennuisMask);
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
