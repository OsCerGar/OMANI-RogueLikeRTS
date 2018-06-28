using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    [SerializeField]
    public List<Power> power = new List<Power>();
    int selectedPower = 0, ennuisMask;

    [SerializeField]
    int maxpowerPool = 100, powerPool;
    float radius = 3;

    private void Start()
    {
        powerPool = 0;
        ennuisMask = 1 << LayerMask.NameToLayer("Interactible");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 6"))
        {
            if (power[selectedPower] != null)
            {
                power[selectedPower].CastPower();
            }
        }
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

        Collider[] targetsInViewRadius = Physics.OverlapSphere(this.transform.position, radius, ennuisMask);
        foreach (Collider col in targetsInViewRadius)
        {
            if (col.name == "Ennui")
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
