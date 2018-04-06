using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;

    // Use this for initialization
    public virtual void Start()
    {
        if (this.GetComponent<Rigidbody>() != null)
        {
            myRigidBody = this.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Action(BoyMovement _boy)
    {

    }

    public void enableRigid()
    {
        myRigidBody.isKinematic = false;
    }

    public void disableRigid()
    {
        myRigidBody.isKinematic = true;

    }
}
