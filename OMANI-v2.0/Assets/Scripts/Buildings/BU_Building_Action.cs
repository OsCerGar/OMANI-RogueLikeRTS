using UnityEngine;

public class BU_Building_Action : Interactible
{

    [SerializeField]
    BU_UniqueBuilding parentResources;
    public bool readyToSpawn { get; set; }
    Animator animator;
    AudioSource pilarmovement, pilarReturned;
    private float actionDone;
    private bool firstTimepowerReduced;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        parentResources = transform.parent.GetComponent<BU_UniqueBuilding>();

        animator = GetComponentInChildren<Animator>();

        //Laser Price
        linkPrice = 20;
        price = 50;
        finalLinkPrice = 40;
        currentLinkPrice = 0;
        t = 0.2f;


        pilarmovement = transform.Find("Sounds").Find("PilarMovement").GetComponent<AudioSource>();
        pilarReturned = transform.Find("Sounds").Find("PilarReturnedProgram").GetComponent<AudioSource>();
    }

    public void BuildingAction()
    {
        parentResources.BuildingAction();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void LateUpdate()
    {
        if (!fullActioned)
        {
            if (!animator.GetBool("Energy") && powerReduced < price)
            {
                animator.Play("PilarDown", 0, powerReduced / price);

                if (Time.time - actionDone > 0.1f)
                {
                    if (powerReduced > 1f)
                    {
                        firstTimepowerReduced = true;
                        pilarReturned.enabled = false;
                        pilarmovement.volume = 0.07f;
                        pilarmovement.pitch = 0.8f;
                    }
                    else
                    {
                        if (firstTimepowerReduced)
                        {
                            firstTimepowerReduced = false;
                            pilarReturned.enabled = true;
                        }
                        pilarmovement.volume = 0f;
                    }
                }
            }

            base.LateUpdate();

        }
        else
        {
            //If the animation is almost finished.
            if (latestFullActionPowerReduced >= 0.95f)
            {
                base.LateUpdate();

                fullActioned = false;
            }

            latestFullActionPowerReduced = Mathf.Lerp(latestFullActionPowerReduced, 1, 0.08f);
            animator.Play("PilarDown", 0, latestFullActionPowerReduced);
        }
    }

    public override void FullAction()
    {
        Debug.Log("fullaction");
        if (parentResources.buildingDistrict.totalEnergyReturn() > parentResources.requiredEnergy)
        {
            readyToSpawn = true;
        }
        else
        {
            readyToSpawn = false;
        }
        if (readyToSpawn)
        {

            base.FullAction();

            fullActioned = true;
        }
        else if (!animator.GetBool("Energy"))
        {
            animator.SetTrigger("NotReady");
        }

    }
    public override void Action()
    {
        if (parentResources.buildingDistrict.totalEnergyReturn() > parentResources.requiredEnergy)
        {
            readyToSpawn = true;
        }
        else
        {
            readyToSpawn = false;
        }

        if (readyToSpawn)
        {
            actionDone = Time.time;
            base.Action();

            pilarmovement.pitch = 1f;
            pilarmovement.volume = 0.5f;

        }
        else if (!animator.GetBool("Energy"))
        {
            animator.SetTrigger("NotReady");
        }
    }

    public override void ActionCompleted()
    {
        BuildingAction();
        parentResources.buildingDistrict.removeEnergy(parentResources.requiredEnergy);
        parentResources.buildingDistrict.energyUpdateReduced();
        base.ActionCompleted();
    }

    public void StopWorkingAnimator()
    {
        animator.SetBool("Energy", true);
    }

}
