using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    [SerializeField]
    public List<Power> power = new List<Power>();
    int selectedPower = 0, ennuisMask;
    PW_SlowMotion slowMo;
    PW_Hearthstone hearthStone;

    [SerializeField]
    public float maxpowerPool = 100, powerPool = 100, increaseAmount = 1;
    float radius = 3;

    private void Start()
    {
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
        slowMo = this.transform.GetComponent<PW_SlowMotion>();
        hearthStone = this.transform.GetComponent<PW_Hearthstone>();

    }

    // Update is called once per frame
    void Update()
    {
        #region Inputs
        #region LaserBeams
        //Controller
        if (Input.GetKeyDown("joystick button 7"))
        {
            //Energy Beam
        }

        if (Input.GetKey("joystick button 6"))
        {
            //Attack mode
            if (Input.GetKeyDown("joystick button 7"))
            {
                //Attack Beam
            }
        }

        //Kb&M
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Attack mode

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Energy Beam
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Attack Beam
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
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick button 0"))
        {
            hearthStone.CastPower();
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

    public void addPower(int amount)
    {
        powerPool = Mathf.Clamp(powerPool + amount, 0, maxpowerPool);
    }

    public bool reducePower(int amount)
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
