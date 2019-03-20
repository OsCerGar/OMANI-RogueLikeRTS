using UnityEngine;

public class BU_EnergyRestoringBuilding : Interactible
{
    public bool hasEnergyToRestore { get; set; }
    Animator animator;
    private float actionDone;
    private bool firstTimepowerReduced;

    SkinnedMeshRenderer renderere;
    float currentLerpTime, lerpTime = 1f;

    [SerializeField]
    float restoringEnergy = 500, initialEnergy = 500;
    float oldEnergyReduced;

    Transform sphere;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        oldEnergyReduced = restoringEnergy;
        initialEnergy = restoringEnergy;
        hasEnergyToRestore = true;
        animator = transform.FindDeepChild("Sphere").GetComponent<Animator>();
        sphere = transform.FindDeepChild("SphereMesh");

        //Laser Price
        linkPrice = 15;
        price = 50;
        finalLinkPrice = 40;
        currentLinkPrice = 0;
        t = 0.2f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    public override void LateUpdate()
    {

        if (actionBool) { animator.SetBool("Energy", true); }
        if (!actionBool || Time.time - actionDone > 1f) { animator.SetBool("Energy", false); }
    }

    private void FullActionLink()
    {
        currentLerpTime += Time.deltaTime;
        float t = currentLerpTime / lerpTime;
        t = Mathf.Sin(t * Mathf.PI * 0.0025f);

        latestFullActionPowerReduced = Mathf.Lerp(latestFullActionPowerReduced, 1, t);
        animator.Play("PilarDown", 0, latestFullActionPowerReduced);
    }

    private void Link()
    {
        /*
        if (!animator.GetBool("Energy") && powerReduced < price)
        {
            animator.Play("PilarDown", 0, powerReduced / price);

            if (Time.time - actionDone > 0.1f)
            {
                if (powerReduced > 1f)
                {
                    firstTimepowerReduced = true;
                }
                else
                {
                    if (firstTimepowerReduced)
                    {
                        firstTimepowerReduced = false;
                    }
                }
            }
        }
        */
    }

    public override void FullAction()
    {
        if (hasEnergyToRestore)
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
        if (hasEnergyToRestore)
        {
            actionDone = Time.time;

            currentLinkPrice = Mathf.Lerp(linkPrice, finalLinkPrice, t);
            t += t * Time.unscaledDeltaTime;

            if (powers.restorePower(currentLinkPrice))
            {
                laserAudio.energyTransmisionSound(currentLinkPrice);
                float restoredEnergy = currentLinkPrice * Time.unscaledDeltaTime;
                restoringEnergy -= restoredEnergy;
                numberPool.NumberSpawn(numbersTransform, restoredEnergy, Color.green, numbersTransform.gameObject, true);


                if (restoringEnergy <= 0)
                {
                    hasEnergyToRestore = false;
                    ActionCompleted();
                }
                else
                {
                    sphere.localScale = new Vector3(restoringEnergy / initialEnergy, restoringEnergy / initialEnergy, restoringEnergy / initialEnergy);
                }

                actionBool = true;
            }
            else
            {
                actionBool = false;
            }

        }
    }

    public override void ActionCompleted()
    {
        restoringEnergy = 0;
        currentLinkPrice = 0;
        t = 0.2f;

        disableButton();

    }

    public void disableButton()
    {
        laserTarget.gameObject.SetActive(false);
        //enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    public void enableButton()
    {
        foreach (Material mat in renderere.materials)
        {
            MK.Toon.MKToonMaterialHelper.SetEmissionColor(mat, Color.green);
            MK.Toon.MKToonMaterialHelper.SetOutlineColor(mat, Color.green);
        }

        laserTarget.gameObject.SetActive(true);
        //enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        Ready();
    }

    public void StopWorkingAnimator()
    {
        //animator.SetBool("Energy", true);
        //currentLinkPrice = 0;
    }
    public void Ready()
    {
        hasEnergyToRestore = true;
    }

}
