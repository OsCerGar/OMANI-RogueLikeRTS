using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour {
    private Vector3 initialPos, openedPos;
	// Use this for initialization
	void Start () {
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        openedPos = new Vector3(transform.Find("Opened").position.x, transform.Find("Opened").position.y, transform.Find("Opened").position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "People")
        {
            transform.position = Vector3.Lerp(openedPos, initialPos, Time.deltaTime);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "People")
        {
            transform.position = Vector3.Lerp(initialPos, openedPos, Time.deltaTime);
        }
    }
}
