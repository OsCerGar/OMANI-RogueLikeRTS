using UnityEngine;

public class BU_Building_Action2 : Interactible
{

    [SerializeField]
    BU_UniqueBuildingNoDistrict parentResources;
    public bool readyToSpawn { get; set; }
    Animator animator;
    AudioSource pilarmovement, pilarReturned;
    private float actionDone;
    private bool firstTimepowerReduced;

    SkinnedMeshRenderer renderere;
    GameObject instructions;
    float currentLerpTime, lerpTime = 1f;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        parentResources = transform.parent.GetComponent<BU_UniqueBuildingNoDistrict>();

        readyToSpawn = true;
        animator = GetComponent<Animator>();

        //Laser Price
        price = 50;
        finalLinkPrice = 40;
        currentLinkPrice = 0;
        t = 0.2f;

        pilarmovement = transform.Find("Sounds").Find("PilarMovement").GetComponent<AudioSource>();
        pilarReturned = transform.Find("Sounds").Find("PilarReturnedProgram").GetComponent<AudioSource>();
        if (transform.Find("Tutorial_Instruction") != null)
        {
            instructions = transform.Find("Tutorial_Instruction").gameObject;
        }
        renderere = GetComponentInChildren<SkinnedMeshRenderer>();
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
                //fullActioned = false;
            }

            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.0025f);

            latestFullActionPowerReduced = Mathf.Lerp(latestFullActionPowerReduced, 1, t);
            animator.Play("PilarDown", 0, latestFullActionPowerReduced);
        }
    }

    public override void FullAction()
    {
        if (readyToSpawn)
        {
            currentLerpTime = 0;
            startTime = Time.time;

            base.FullAction();

            fullActioned = true;
        }
        else if (!animator.GetBool("Energy"))
        {
            //animator.SetTrigger("NotReady");
        }

    }
    public override void Action()
    {

        if (readyToSpawn)
        {
            actionDone = Time.time;
            base.Action();

            pilarmovement.pitch = 1f;
            pilarmovement.volume = 0.5f;

        }
        else if (!animator.GetBool("Energy"))
        {
            //animator.SetTrigger("NotReady");
        }
    }

    public override void ActionCompleted()
    {
        BuildingAction();
        readyToSpawn = false;
        fullActioned = false;
        base.ActionCompleted();
    }

    public void StopWorkingAnimator()
    {
        animator.SetBool("Energy", true);
        currentLinkPrice = 0;
    }
    public void Ready()
    {
        readyToSpawn = true;
    }

}
