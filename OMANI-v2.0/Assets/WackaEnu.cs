using System.Collections;
using UnityEngine;

public class WackaEnu : Interactible
{
    Animator animator;
    BU_Energy_CityDistricts wackaEnuDistrict;
    public float energyCableAnimationTime = 2.35f;


    //ENU TYPE VARIABLES 
    public SkinnedMeshRenderer meshRenderer;
    [SerializeField]
    Material enuMaterial, goldMaterial, blackMaterial;
    int energyToAdd = 1;
    bool black;
    BU_Energy electricCITY;
    public bool OUT;

    private void Initializer()
    {
        linkPrice = 3;
        finalLinkPrice = 10;

        price = 10;
        wackaEnuDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        animator = GetComponentInChildren<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        electricCITY = FindObjectOfType<BU_Energy>();
    }
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        Initializer();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        /*
        if (!animator.GetBool("Done"))
        {
            animator.Play("wackaEnuColor", 0, powerReduced / price);
        }
        */

        if (OUT)
        {
            animGetOut();
            OUT = false;
        }
    }

    public override void Update()
    {
    }
    public override void Action()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ENUPOTEN"))
        {
            base.Action();
        }
    }
    public override void FullAction()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ENUPOTEN"))
        {
            base.FullAction();
        }
    }
    public override void ActionCompleted()
    {
        animator.SetBool("Done", true);
        base.ActionCompleted();

        if (!black)
        {
            StartCoroutine("AddEnergy");
        }

        else
        {
            //removeCables
            electricCITY.BuildingAction();
        }
    }

    IEnumerator AddEnergy()
    {
        yield return new WaitForSeconds(energyCableAnimationTime);

        //Play energy cable animation
        wackaEnuDistrict.addEnergyCityDistrict(energyToAdd);
    }

    public Animator GetAnimator()
    {
        return animator;
    }
    public void resetEnu()
    {
        animator.SetBool("Done", false);
        //resets power reduced and stuff
        base.ActionCompleted();

        //Selects the type of enu
        enuRandomizer();
    }

    private void enuRandomizer()
    {
        black = false;

        float random = Random.Range(0f, 1f);


        //regularenu
        if (random < 0.9f)
        {
            meshRenderer.material = enuMaterial;
            energyToAdd = 1;

        }
        //blackennu
        else if (random >= 0.9f && random < 0.96f)
        {
            meshRenderer.material = blackMaterial;
            energyToAdd = 0;
            black = true;
        }
        //goldennu
        else
        {
            meshRenderer.material = goldMaterial;
            energyToAdd = 3;
        }

    }

    public void animGetOut()
    {
        animator.SetTrigger("GetOut");
    }
}
