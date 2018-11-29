using System.Collections;
using UnityEngine;

public class WackaEnu : Interactible
{
    Animator animator;
    BU_Energy_CityDistricts wackaEnuDistrict;
    public float energyCableAnimationTime = 2.35f;

    private void Initializer()
    {
        linkPrice = 3;
        finalLinkPrice = 10;

        price = 10  ;
        wackaEnuDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        animator = GetComponent<Animator>();
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
        if (!animator.GetBool("Done"))
        {
            animator.Play("wackaEnuColor", 0, powerReduced / price);
        }
    }
    public override void Update()
    {
    }

    public override void ActionCompleted()
    {
        animator.SetBool("Done", true);
        base.ActionCompleted();
        StartCoroutine("AddEnergy");
    }

    IEnumerator AddEnergy()
    {
        yield return new WaitForSeconds(energyCableAnimationTime);

        //Play energy cable animation
        wackaEnuDistrict.addEnergyCityDistrict(1);
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
    }
}
