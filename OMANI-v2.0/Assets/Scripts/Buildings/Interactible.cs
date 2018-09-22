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
    public float startTime, currentLinkPrice, t, finalLinkPrice;
    public bool actionBool { get; set; }

    public Powers powers = null;
    public PowerManager powerManager;
    public MetaAudioController laserAudio;

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

        powerReduced = Mathf.Clamp(powerReduced, 0, price);
    }

    public virtual void Action()
    {
        currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
        t += t * Time.unscaledDeltaTime;


        startTime = Time.time;

        if (powers.reducePower(currentLinkPrice))
        {
            laserAudio.energyTransmisionSound(currentLinkPrice);
            powerReduced += currentLinkPrice * Time.unscaledDeltaTime;
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
        currentLinkPrice = 0;
        t = 0.2f;

    }

    public virtual void ReducePower()
    {
        powerReduced -= 5 * Time.unscaledDeltaTime;
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
