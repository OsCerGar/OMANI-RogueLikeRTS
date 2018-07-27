using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;

    public bool hasRigid { get; set; }

    public float price { get; set; }
    public float powerReduced { get; set; }
    public float linkPrice = 5;
    private float startTime;


    Powers powers = null;
    PowerManager powerManager;

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
        if (Time.time - startTime > 3f && powerReduced > 1)
        {
            ReducePower();
        }

        if (powerReduced >= price)
        {
            ActionCompleted();
        }
    }

    public virtual void Action()
    {
        startTime = Time.time;
        if (powers.reducePower(linkPrice * Time.unscaledDeltaTime))
        {
            powerReduced += linkPrice * Time.unscaledDeltaTime;
            Debug.Log("action" + powerReduced);

        }
    }

    public virtual void ActionCompleted()
    {
        powerReduced = 0;
    }

    public void ReducePower()
    {
        powerReduced -= 1 * Time.unscaledDeltaTime;
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
