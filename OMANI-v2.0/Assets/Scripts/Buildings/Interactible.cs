using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;
    bool hasRigid = false;
    float price;

    // Use this for initialization
    public virtual void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody>();

        if (myRigidBody != null)
        {
            hasRigid = true;
        }
    }

    public virtual void Action()
    {

    }

    public void enableRigid()
    {
        if (hasRigid)
        {
            myRigidBody.isKinematic = false;
        }
    }

    public void disableRigid()
    {
        if (hasRigid)
        {
            myRigidBody.isKinematic = true;
        }
    }
}
