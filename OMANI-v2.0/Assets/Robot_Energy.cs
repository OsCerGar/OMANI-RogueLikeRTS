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
        _robot = GetComponent<Robot>();
        workerSM = GetComponentInChildren<WorkerSM>();
        price = 75;
        linkPrice = 2;
        finalLinkPrice = 7;
        currentLinkPrice = 0;
        t = 0.2f;
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
                _robot.powerPool += currentLinkPrice * Time.unscaledDeltaTime;
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
        _robot.AutoReclute();
        startTime = Time.time;
        ready = true;
        currentLinkPrice = 0;
        t = 0.2f;
        //Plays the selection Sound
    }

    public override void ReducePower()
    {
        _robot.powerPool -= 1 * Time.unscaledDeltaTime;
    }
}
