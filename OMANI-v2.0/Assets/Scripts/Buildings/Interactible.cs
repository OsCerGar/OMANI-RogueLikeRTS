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
    public float linkPrice = 5;
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


    public virtual void Initialize()
    {
        myRigidBody = GetComponent<Rigidbody>();
        powerManager = FindObjectOfType<PowerManager>();
        powers = FindObjectOfType<Powers>();
        laserAudio = FindObjectOfType<MetaAudioController>();
        numberPool = FindObjectOfType<NumberPool>();
        if (transform.Find("UI") && transform.Find("UI").Find("Numbers"))
        {
            numbersTransform = transform.Find("UI").Find("Numbers");
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
        if (Time.time - startTime > 3f && powerReduced > 0)
        {
            ReducePower();
        }
    }

    public virtual void LateUpdate()
    {

        if (powerReduced >= price - 2f)
        {
            ActionCompleted();
        }

        powerReduced = Mathf.Clamp(powerReduced, 0, price);
    }

    public virtual void Action()
    {
        currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
        t += t * Time.unscaledDeltaTime;

        float oldPowerReduced = powerReduced;

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
        if (powerReduced - Mathf.Floor(oldPowerReduced) >= 0.95f)
        {
            numberPool.NumberSpawn(numbersTransform, 1, Color.cyan, gameObject);
        }

    }

    public virtual void FullAction()
    {
        latestFullActionPowerReduced = powerReduced / price;
        numberPool.NumberSpawn(numbersTransform, latestFullActionPowerReduced, Color.cyan, gameObject);

        powerReduced += powers.reduceAsMuchPower(price - powerReduced);
        //In case it gets close, free energy for the people
        if (price - powerReduced < 2f)
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
        powerReduced -= 5 * Time.unscaledDeltaTime;
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
