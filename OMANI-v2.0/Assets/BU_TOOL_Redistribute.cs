using System.Collections.Generic;
using UnityEngine;


public class BU_TOOL_Redistribute : HE
{

    public BU_Energy_CityDistricts parentDistrict;
    public List<BU_Energy_CityDistricts> otherDistricts = new List<BU_Energy_CityDistricts>();
    Animator anim;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        requiredEnergy = 10;
        parentDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        otherDistricts.AddRange(transform.root.GetComponentsInChildren<BU_Energy_CityDistricts>());
        otherDistricts.Remove(parentDistrict);
        anim = transform.Find("HRedistribute").GetComponent<Animator>();
    }

    public override void BuildingAction()
    {
        base.BuildingAction();

        anim.SetTrigger("Activate");
    }

    public override void Action()
    {
        parentDistrict.removeEnergy(50);
        parentDistrict.energyUpdateReduced();

        foreach (BU_Energy_CityDistricts district in otherDistricts)
        {
            district.addEnergyCityDistrict(15);
        }
    }


}
