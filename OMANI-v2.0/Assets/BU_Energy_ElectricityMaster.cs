using System.Collections;
using UnityEngine;

public class BU_Energy_ElectricityMaster : MonoBehaviour
{
    [SerializeField]
    BU_Energy_District EnergyDistrict1, EnergyDistrict2, EnergyDistrict3, EnergyDistrict4;
    [SerializeField]
    BU_Energy_CityDistricts CityDistricts1, CityDistricts2, CityDistricts3, CityDistricts4;
    private int totalDistrictEnergy;
    private int i = 0;

    // Use this for initialization
    void Start()
    {

        #region EnergyDistrictInitializer
        EnergyDistrict1 = transform.Find("District1").GetComponent<BU_Energy_District>();
        EnergyDistrict2 = transform.Find("District2").GetComponent<BU_Energy_District>();
        EnergyDistrict3 = transform.Find("District3").GetComponent<BU_Energy_District>();
        EnergyDistrict4 = transform.Find("District4").GetComponent<BU_Energy_District>();
        #endregion

        #region BuildingDistrictInitializer

        CityDistricts1 = transform.root.Find("District1").GetComponent<BU_Energy_CityDistricts>();
        CityDistricts2 = transform.root.Find("District2").GetComponent<BU_Energy_CityDistricts>();
        CityDistricts3 = transform.root.Find("District3").GetComponent<BU_Energy_CityDistricts>();
        CityDistricts4 = transform.root.Find("District4").GetComponent<BU_Energy_CityDistricts>();
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
        if (EnergyDistrict2 != null)
        {
            //Secondary conexions
            totalDistrictEnergy += EnergyDistrict2.returnEnergyWithoutMain();
        }
        if (EnergyDistrict4 != null)
        {
            totalDistrictEnergy += EnergyDistrict4.returnEnergyFull();

            CityDistricts4.totalEnergyUpdate(totalDistrictEnergy);
        }
        totalDistrictEnergy = 0;

    }

    private void TotalDistrictEnergy3()
    {
        if (EnergyDistrict4 != null)
        {
            //Secondary conexions
            totalDistrictEnergy += EnergyDistrict4.returnEnergyWithoutMain();
        }
        if (EnergyDistrict3 != null)
        {
            totalDistrictEnergy += EnergyDistrict3.returnEnergyFull();

            CityDistricts3.totalEnergyUpdate(totalDistrictEnergy);
        }
        totalDistrictEnergy = 0;

    }

    private void TotalDistrictEnergy2()
    {

        if (EnergyDistrict1 != null)
        {
            //Secondary conexions
            totalDistrictEnergy += EnergyDistrict1.returnEnergyWithoutMain();
        }
        if (EnergyDistrict2 != null)
        {
            totalDistrictEnergy += EnergyDistrict2.returnEnergyFull();
            CityDistricts2.totalEnergyUpdate(totalDistrictEnergy);
        }

        totalDistrictEnergy = 0;

    }

    private void TotalDistrictEnergy1()
    {

        if (EnergyDistrict3 != null)
        {

            //Secondary conexions
            totalDistrictEnergy += EnergyDistrict3.returnEnergyWithoutMain();
        }

        if (EnergyDistrict1 != null)
        {

            totalDistrictEnergy += EnergyDistrict1.returnEnergyFull();
            CityDistricts1.totalEnergyUpdate(totalDistrictEnergy);
        }

        totalDistrictEnergy = 0;
    }
}
