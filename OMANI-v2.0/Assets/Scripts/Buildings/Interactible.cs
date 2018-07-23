using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;

    public bool hasRigid { get; set; }
    public bool link { get; set; }

    public float price { get; set; }
    public float powerReduced { get; set; }
    public float linkPrice = 5;
    

    Powers powers = null;
    PowerManager powerManager;
    public Link linky { get; set; }

    public virtual void Initialize()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();

    }

    public virtual void Awake()
    {
        Initialize();
    }

    // Use this for initialization
    public virtual void Start()
    {
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
        if (!link)
        {
            CreateLink();
        }
    }

    public virtual void ActionCompleted()
    {
        linky.Completed();
        link = false;
        powerReduced = 0;
    }

    public void CreateLink()
    {
        //CreatesLink
        linky = powerManager.CreateLink(this.transform, powers).GetComponent<Link>();
        linky.power = powers.gameObject;
        linky.interactible = this.transform.gameObject;
        link = true;
    }

    public void DestroyLink()
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
