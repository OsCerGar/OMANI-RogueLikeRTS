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
            yield return new WaitForSeconds(0.1f);
            // execute block of code here
            totalDistrictEnergy = 0;
            switch (i)
            {
                case 0:
                    totalDistrictEnergy += EnergyDistrict[0].returnEnergyFull();
                    //Secondary conexions
                    totalDistrictEnergy += EnergyDistrict[2].returnEnergyWithoutMain();

                    CityDistricts[i].totalEnergyUpdate(totalDistrictEnergy);

                    i++;

                    break;
                case 1:
                    totalDistrictEnergy += EnergyDistrict[1].returnEnergyFull();

                    //Secondary conexions
                    totalDistrictEnergy += EnergyDistrict[0].returnEnergyWithoutMain();

                    CityDistricts[i].totalEnergyUpdate(totalDistrictEnergy);

                    i++;

                    break;
                case 2:
                    totalDistrictEnergy += EnergyDistrict[2].returnEnergyFull();
                    //Secondary conexions
                    totalDistrictEnergy += EnergyDistrict[3].returnEnergyWithoutMain();

                    CityDistricts[i].totalEnergyUpdate(totalDistrictEnergy);

                    i++;

                    break;
                case 3:
                    totalDistrictEnergy += EnergyDistrict[3].returnEnergyFull();
                    //Secondary conexions
                    totalDistrictEnergy += EnergyDistrict[1].returnEnergyWithoutMain();

                    CityDistricts[i].totalEnergyUpdate(totalDistrictEnergy);

                    i++;

                    break;

                default:
                    //resets it
                    i = 0;
                    break;
            }
        }
    }

}
