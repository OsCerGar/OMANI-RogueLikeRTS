using UnityEngine;
using UnityEngine.UI;
public class Flag : Interactible
{
    int armor;
    ShootingPositionFlag shootingPositionFlag;
    Robot robotConnected;
    int playerLayer = 1 << 9;
    bool pickingUp;
    float pickingUpTimer, timeToPickUp = 3f;

    Image clockImage;

    private void Start()
    {
        shootingPositionFlag = transform.FindDeepChild("ShootingPosition").GetComponent<ShootingPositionFlag>();
        clockImage = transform.Find("UI").Find("PowerClock").Find("Clock").GetComponent<Image>();
        //Laser Price
        linkPrice = 15;
        price = 50;
        finalLinkPrice = 25;
        currentLinkPrice = 0;

        t = 0.2f;
    }

    public override void Update()
    {
        if (Time.time - startTime > 2f && powerReduced > 0)
        {
            ReducePower(2);
            // when power reduced is 0, robot disables
            if (powerReduced <= 0.1f)
            {
                if (!robotConnected.anim.GetCurrentAnimatorStateInfo(0).IsName("Disconnected"))
                {
                    robotConnected.CoolDown();
                }

            }

        }

        //If player is nearby start picking up flag.
        FindPlayer();
        if (pickingUp)
        {
            pickingUpFlag();

            clockImage.color = Color.green;
            clockImage.fillAmount = pickingUpTimer / timeToPickUp;

        }
        else
        {
            clockImage.color = Color.cyan;
            clockImage.fillAmount = powerReduced / price;
        }

    }
    public override void LateUpdate()
    {
        powerReduced = Mathf.Clamp(powerReduced, 0, price);
    }

    public void Thrown(Robot _robotToThrow)
    {
        robotConnected = _robotToThrow;
        shootingPositionFlag.MyRobot = robotConnected;
        robotConnected.Deploy(shootingPositionFlag.gameObject);
        powerReduced = 50;
    }


    private void pickingUpFlag()
    {
        pickingUpTimer += Time.deltaTime;

        if (pickingUpTimer > timeToPickUp)
        {
            //pick Ups flag
            PickUp();
        }
    }

    public void PickUp()
    {
        FlagThrowing.flagThrowing.PickUpFlag(this);

        //reclute robot if not disabled
        if (!robotConnected.anim.GetCurrentAnimatorStateInfo(0).IsName("Disconnected"))
        {
            Army.army.Reclute(robotConnected);
        }
        pickingUpTimer = 0;
        //remove robot
        robotConnected = null;
    }

    private void FindPlayer()
    {    // On trigger stay, it starts a countdown to get picked up.
        bool foundPlayer = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, 5f, playerLayer);
        foreach (Collider col in targetsInViewRadius)
        {
            if (col.tag == "Player")
            {
                foundPlayer = true;
            }
        }

        if (foundPlayer) { pickingUp = true; }
        else
        {
            pickingUp = false; pickingUpTimer = 0;
        }
    }

}
