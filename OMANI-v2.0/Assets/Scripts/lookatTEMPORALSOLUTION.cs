using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatTEMPORALSOLUTION : MonoBehaviour
{

    public Army commander;

    public string selectedType = "Swordsman";

    // Use this for initialization
    void Start()
    {
        commander = FindObjectOfType<Army>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(commander.transform);

        //En un futuro, R2/L2
        if (Input.GetKey("joystick button 5") || Input.GetMouseButtonDown(1))
        {
            commander.Order(selectedType, this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //En un futuro, R2/L2
        if (Input.GetKey("joystick button 4") || Input.GetMouseButtonDown(0))
        {
            if (other.tag == "People")
            {
                commander.Reclute(other.GetComponent<NPC>());
            }
        }
    }
}
