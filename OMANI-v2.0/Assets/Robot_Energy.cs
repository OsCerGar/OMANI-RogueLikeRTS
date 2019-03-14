using UnityEngine;

public class Robot_Energy : Interactible
{
    [SerializeField]
    public bool ready { get; set; }
    public Robot _robot;
    WorkerSM workerSM;


    public override void Start()
    {
        base.Start();
        price = 75;
        linkPrice = 2;
        finalLinkPrice = 7;
        currentLinkPrice = 0;
        t = 0.2f;
        _robot = GetComponent<Robot>();
        workerSM = _robot.workerSM;

    }

    public override void Action()
    {
        currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
        t += t * Time.unscaledDeltaTime;
        float oldPowerPool = _robot.powerPool;

        if (!ready)
        {
            startTime = Time.time;

            if (powers.reducePower(currentLinkPrice))
            {
                _robot.powerPool += currentLinkPrice * Time.unscaledDeltaTime;

                actionBool = true;
            }
            else
            {
                actionBool = false;
            }
        }
        if (_robot.powerPool - Mathf.Floor(oldPowerPool) >= 0.95f)
        {
            numberPool.NumberSpawn(numbersTransform, 1, Color.cyan, gameObject);
        }
    }

    public override void FullAction()
    {
        _robot.powerPool = powers.reduceAsMuchPower(_robot.maxpowerPool);

        numberPool.NumberSpawn(numbersTransform, powers.reduceAsMuchPower(_robot.maxpowerPool), Color.cyan, gameObject);

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
