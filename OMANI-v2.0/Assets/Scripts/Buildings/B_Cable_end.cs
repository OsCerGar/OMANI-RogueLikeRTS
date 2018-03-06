using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Cable_end : MonoBehaviour
{
    public temporalCable cable;
    // Use this for initialization
    void Start()
    {
        cable = this.transform.parent.GetComponent<temporalCable>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (cable.energy == false) {
            this.transform.parent = cable.transform;
        }
        */
    }
}
