using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;

    public bool hasRigid { get; set; }

    public float price { get; set; }
    [SerializeField]
    public float powerReduced;
    public float linkPrice = 5;
    private float startTime;
    public bool actionBool { get; set; }

    public Powers powers = null;
    PowerManager powerManager;
    MetaAudioController laserAudio;

    public virtual void Initialize()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        laserAudio = FindObjectOfType<MetaAudioController>();
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
        if (Time.time - startTime > 3f && powerReduced > 0)
        {
            ReducePower();
        }
    }

    public virtual void LateUpdate()
    {
        if (powerReduced >= price)
        {
            ActionCompleted();
        }
    }

    public virtual void Action()
    {
        startTime = Time.time;

        if (powers.reducePower(linkPrice))
        {
            laserAudio.energyTransmisionSound(linkPrice);
            powerReduced += linkPrice * Time.unscaledDeltaTime;
            actionBool = true;
        }
        else
        {
            actionBool = false;
        }
    }


    public virtual void ActionCompleted()
    {
        powerReduced = 0;
    }

    public virtual void ReducePower()
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
