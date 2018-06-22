using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtSomething : MonoBehaviour {

    [SerializeField]Transform ThingToLook ;
    // Use this for initialization

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.LookAt(ThingToLook);
    }
}
