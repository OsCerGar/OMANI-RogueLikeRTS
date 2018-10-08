using UnityEngine;

public class BU_Energy_CityDistricts : MonoBehaviour
{
    private BU_UniqueBuilding building;
    private int totalEnergy;

    private BU_District_Animations animationsManager;
    // Use this for initialization
    void Start()
    {
        building = transform.GetComponentInChildren<BU_UniqueBuilding>();
        animationsManager = transform.GetComponentInChildren<BU_District_Animations>();
    }

    public BU_UniqueBuilding returnBuildings()
    {
        return building;
    }
    public int totalEnergyReturn()
    {
        return totalEnergy;
    }
    public void totalEnergyUpdate(int _totalEnergy)
    {
        totalEnergy = _totalEnergy;
        building.totalEnergy = _totalEnergy;

        animationsManager.energyLevel(_totalEnergy);
    }
}
