using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_TOOL_QR : BU_UniqueBuilding
{

    public BU_Energy_CityDistricts parentDistrict;
    PeoplePool peoplePool;
    public List<Transform> QR = new List<Transform>();

    int lastRandom;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        requiredEnergy = 50;
        parentDistrict = GetComponentInParent<BU_Energy_CityDistricts>();
        peoplePool = FindObjectOfType<PeoplePool>();

        foreach (Transform child in transform.Find("QRs"))
        {
            QR.Add(child);
        }

    }

    public override void BuildingAction()
    {
        base.BuildingAction();

        RandomQR();
    }

    public void RandomQR()
    {
        //Pay the cost
        parentDistrict.removeEnergy(50);
        parentDistrict.energyUpdateReduced();

        //Roll the dice
        int random = Random.Range(0, QR.Count);

        while (random == lastRandom)
        {
            random = Random.Range(0, QR.Count);
            Debug.Log(random + "" + QR.Count);

        }
        QR[random].gameObject.SetActive(true);
        lastRandom = random;

        StartCoroutine("RemoveQR");
    }

    IEnumerator RemoveQR()
    {
        yield return new WaitForSeconds(10);

        //Play energy cable animation
        QR[lastRandom].gameObject.SetActive(false);
    }

}
