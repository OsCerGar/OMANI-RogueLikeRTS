using UnityEngine;

public class Interactible : MonoBehaviour
{

    Rigidbody myRigidBody;
    [HideInInspector]
    public bool hasRigid { get; set; }
    [HideInInspector]
    public float price { get; set; }
    [SerializeField]
    public float powerReduced;
    [HideInInspector]
    public float linkPrice = 8;
    public float startTime, currentLinkPrice, t, finalLinkPrice, latestFullActionPowerReduced, animationFullActionState;
    [HideInInspector]
    public bool actionBool { get; set; }
    public bool fullActioned;

    [HideInInspector]
    public Powers powers = null;
    [HideInInspector]
    public PowerManager powerManager;
    [HideInInspector]
    public MetaAudioController laserAudio;

    [HideInInspector]
    public NumberPool numberPool;
    public Transform numbersTransform;
    public Transform laserTarget;


    public virtual void Initialize()
    {
        laserTarget = transform.FindDeepChild("LaserObjective");

        myRigidBody = GetComponent<Rigidbody>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        laserAudio = FindObjectOfType<MetaAudioController>();
        numberPool = FindObjectOfType<NumberPool>();
        if (transform.FindDeepChild("UI"))
        {
            numbersTransform = transform.FindDeepChild("Numbers");
        }
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
        if (Time.time - startTime > 5f && powerReduced > 0)
        {
            ReducePower();
        }
    }

    public virtual void LateUpdate()
    {

        if (powerReduced >= price - 5f)
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
            float reduceamount = currentLinkPrice * Time.unscaledDeltaTime;

            powerReduced += reduceamount;
            numberPool.NumberSpawn(numbersTransform, reduceamount, Color.cyan, numbersTransform.gameObject, true);

            actionBool = true;
        }
        else
        {
            actionBool = false;
        }
    }

    public virtual void FullAction()
    {
        latestFullActionPowerReduced = powerReduced / price;
        numberPool.NumberSpawn(numbersTransform, latestFullActionPowerReduced, Color.cyan, numbersTransform.gameObject, true);

        powerReduced += powers.reduceAsMuchPower(price - powerReduced);
        //In case it gets close, free energy for the people
        if (price - powerReduced < 5f)
        {
            powerReduced += 5;
        }
        laserAudio.energyTransmisionSound(currentLinkPrice);

    }


    public virtual void ActionCompleted()
    {
        powerReduced = 0;
        currentLinkPrice = 0;
        t = 0.2f;
    }

    public virtual void ReducePower()
    {
        powerReduced -= 8 * Time.unscaledDeltaTime;
    }

    public virtual void ReducePower(int _reducePower)
    {
        powerReduced -= _reducePower * Time.unscaledDeltaTime;
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
