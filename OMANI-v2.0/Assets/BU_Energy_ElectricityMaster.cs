using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Energy_ElectricityMaster : MonoBehaviour
{
    [SerializeField]
    List<BU_Energy_District> EnergyDistrict = new List<BU_Energy_District>();
    [SerializeField]
    List<BU_Energy_CityDistricts> CityDistricts = new List<BU_Energy_CityDistricts>();
    private int totalDistrictEnergy;
    private int i = 0;

    // Use this for initialization
    void Start()
    {

        #region EnergyDistrictInitializer
        EnergyDistrict.Add(transform.Find("District1").GetComponent<BU_Energy_District>());
        EnergyDistrict.Add(transform.Find("District2").GetComponent<BU_Energy_District>());
        EnergyDistrict.Add(transform.Find("District3").GetComponent<BU_Energy_District>());
        EnergyDistrict.Add(transform.Find("District4").GetComponent<BU_Energy_District>());
        #endregion

        #region BuildingDistrictInitializer
        CityDistricts.Add(transform.root.Find("District1").GetComponent<BU_Energy_CityDistricts>());
        CityDistricts.Add(transform.root.Find("District2").GetComponent<BU_Energy_CityDistricts>());
        CityDistricts.Add(transform.root.Find("District3").GetComponent<BU_Energy_CityDistricts>());
        CityDistricts.Add(transform.root.Find("District4").GetComponent<BU_Energy_CityDistricts>());
        #endregion
        StartCoroutine("EnergyCheck");
    }

    IEnumerator EnergyCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            // execute block of code here
            TotalDistrictEnergy1();
            TotalDistrictEnergy2();
            TotalDistrictEnergy3();
            TotalDistrictEnergy4();
        }

    }

    private void TotalDistrictEnergy4()
    {
        totalDistrictEnergy += EnergyDistrict[3].returnEnergyFull();
        //Secondary conexions
        totalDistrictEnergy += EnergyDistrict[1].returnEnergyWithoutMain();

        CityDistricts[3].totalEnergyUpdate(totalDistrictEnergy);
        totalDistrictEnergy = 0;

    }

    private void TotalDistrictEnergy3()
    {
        totalDistrictEnergy += EnergyDistrict[2].returnEnergyFull();
        //Secondary conexions
        totalDistrictEnergy += EnergyDistrict[3].returnEnergyWithoutMain();

        CityDistricts[2].totalEnergyUpdate(totalDistrictEnergy);
        totalDistrictEnergy = 0;

    }

    private void TotalDistrictEnergy2()
    {
        totalDistrictEnergy += EnergyDistrict[1].returnEnergyFull();

        //Secondary conexions
        totalDistrictEnergy += EnergyDistrict[0].returnEnergyWithoutMain();

        CityDistricts[1].totalEnergyUpdate(totalDistrictEnergy);
        totalDistrictEnergy = 0;

    }

    private void TotalDistrictEnergy1()
    {
        totalDistrictEnergy += EnergyDistrict[0].returnEnergyFull();
        //Secondary conexions
        totalDistrictEnergy += EnergyDistrict[2].returnEnergyWithoutMain();

        CityDistricts[0].totalEnergyUpdate(totalDistrictEnergy);
        totalDistrictEnergy = 0;
    }
}
