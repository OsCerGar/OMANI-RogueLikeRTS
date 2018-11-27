using System.Collections;
using UnityEngine;

public class BU_Energy_CityDistricts : MonoBehaviour
{
    private BU_UniqueBuilding building;
    private int totalEnergy;

    private BU_District_Animations animationsManager;
    public BU_Energy_District energyDistrict;
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
    public void removeEnergy(int _quantity)
    {
        totalEnergy -= _quantity;
    }

    public void addEnergyCityDistrict(int _energy)
    {
        totalEnergy += _energy;
    }
    public void totalEnergyUpdate(int _totalEnergy)
    {
        //building.totalEnergy = _totalEnergy;
        //animationsManager.energyLevel(_totalEnergy);

        animationsManager.repeaterAnimation();
        StartCoroutine("AddEnergy", _totalEnergy);
    }
    public void energyUpdateReduced()
    {
        animationsManager.totalEnnus(totalEnergy);
    }

    IEnumerator AddEnergy(int _energy)
    {
        yield return new WaitForSeconds(2.35f);

        addEnergyCityDistrict(_energy);
        animationsManager.totalEnnus(totalEnergy);
    }


}
