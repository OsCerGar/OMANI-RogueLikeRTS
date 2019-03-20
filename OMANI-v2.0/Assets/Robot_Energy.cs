using UnityEngine;

public class Robot_Energy : Interactible
{
    [SerializeField]
    public bool ready { get; set; }
    public Robot _robot;
    WorkerSM workerSM;
    float oldPowerPool;
    bool oldPowerPoolReset;

    public override void Start()
    {
        base.Start();
        _robot = GetComponent<Robot>();

        price = _robot.maxpowerPool;
        linkPrice = 15;
        finalLinkPrice = 60;
        currentLinkPrice = 0;
        t = 0.2f;
        workerSM = _robot.workerSM;

    }

    public override void Action()
    {
        currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
        t += t * Time.unscaledDeltaTime;

        if (!ready)
        {
            startTime = Time.time;

            if (powers.reducePower(currentLinkPrice))
            {
                float reduceamount = currentLinkPrice * Time.unscaledDeltaTime;
                _robot.powerPool += reduceamount;
                numberPool.NumberSpawn(_robot.numbersTransform, reduceamount, Color.cyan, gameObject, true);

                actionBool = true;
            }
            else
            {
                actionBool = false;
            }
        }


    }

    public override void FullAction()
    {
        _robot.powerPool = powers.reduceAsMuchPower(_robot.maxpowerPool);

        numberPool.NumberSpawn(_robot.numbersTransform, powers.reduceAsMuchPower(_robot.maxpowerPool), Color.cyan, gameObject, true);

        laserAudio.energyTransmisionSound(currentLinkPrice);
    }


    public override void Update()
    {
        if (_robot.powerPool >= _robot.maxpowerPool)
        {
            if (!ready)
            {
                ActionCompleted();
            }
        }
        else
        {
            ready = false;
        }
    }

    public override void ActionCompleted()
    {
        workerSM.selectionRobot();

        startTime = Time.time;
        ready = true;
        currentLinkPrice = 0;
        t = 0.2f;

        //When energy full
        _robot.AutoReclute();

    }

    public override void ReducePower()
    {
        _robot.powerPool -= 1 * Time.unscaledDeltaTime;
    }
}
