using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Wall_Door : MonoBehaviour
{

    Animation door;

    //false = down, true = up
    public bool state = false;
    public float doorCounter;

    // Use this for initialization
    void Start()
    {
        door = this.GetComponent<Animation>();
    }

    private void Update()
    {

        doorCounter += Time.deltaTime;

        if (doorCounter > 15f && state == true)
        {
            DoorDown();
            state = false;
        }
    }

    public void DoorUp()
    {
        door.Play("Door_up");
        state = true;
        doorCounter = 0;
    }

    public void DoorDown()
    {
        door.Play("Door_down");
        state = false;
        doorCounter = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("People"))
        {
            if (state == false && doorCounter > 3.5f)
            {
                DoorUp();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("People"))
        {
            doorCounter = 0;
        }
    }

}
