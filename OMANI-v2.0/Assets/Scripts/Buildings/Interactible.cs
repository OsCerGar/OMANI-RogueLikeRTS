using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;
    bool hasRigid = false, link = false;
    public float price, powerReduced = 0, linkPrice = 1;
    Powers powers = null;
    PowerManager powerManager;
    Link linky;

    // Use this for initialization
    public virtual void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();

        if (myRigidBody != null)
        {
            hasRigid = true;
        }
    }

    public virtual void Update()
    {
        if (link)
        {
            if (powers.reducePower(linkPrice * Time.unscaledDeltaTime))
            {
                powerReduced += linkPrice * Time.unscaledDeltaTime;
            }

            else
            {
                DestroyLink();
            }

            if (powerReduced >= price)
            {
                ActionCompleted();
            }
        }
    }

    public virtual void Action()
    {
        CreateLink();
    }

    public virtual void ActionCompleted()
    {
        linky.Completed();
        link = false;
        powerReduced = 0;
    }

    private void CreateLink()
    {
        //CreatesLink
        linky = powerManager.CreateLink(this.transform, powers).GetComponent<Link>();

        linky.power = powers.gameObject;
        linky.interactible = this.transform.gameObject;
        link = true;
    }

    private void DestroyLink()
    {
        linky.Failed();
        link = false;
        powerReduced = 0;
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
