using System.Collections.Generic;


public class BU_TOOL_Redistribute : BU_UniqueBuilding
{

    public BU_Energy_CityDistricts parentDistrict;
    public List<BU_Energy_CityDistricts> otherDistricts = new List<BU_Energy_CityDistricts>();

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        requiredEnergy = 50;
        parentDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        otherDistricts.AddRange(transform.root.GetComponentsInChildren<BU_Energy_CityDistricts>());
        otherDistricts.Remove(parentDistrict);

    }

    public override void BuildingAction()
    {
        base.BuildingAction();

        Redistribute();
    }

    public void Redistribute()
    {
        parentDistrict.removeEnergy(50);
        parentDistrict.energyUpdateReduced();

        foreach (BU_Energy_CityDistricts district in otherDistricts)
        {
            district.addEnergyCityDistrict(15);
        }
    }


}
