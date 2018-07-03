using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    [SerializeField]
    public List<Power> power = new List<Power>();
    int selectedPower = 0, ennuisMask;
    PW_SlowMotion slowMo;

    [SerializeField]
    public int maxpowerPool = 100, powerPool;
    float radius = 3;

    private void Start()
    {
        powerPool = 100;
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
        slowMo = this.transform.GetComponent<PW_SlowMotion>();
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
        }
        #endregion

        #endregion

    }

    public void addPower(int amount)
    {
        powerPool = Mathf.Clamp(powerPool + amount, 0, maxpowerPool);
    }

    public bool reducePower(int amount)
    {
        Debug.Log(amount);
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
