using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_Resources : BU_UniqueBuilding
{
    //State of the lever.
    public bool state = false;
    private Transform scrapMaker, workerMaker;

    [SerializeField]
    private GameObject scrap, worker;

    public float timeToSpawnScrap = 30, timeToSpawnWorker = 45, timeToSpawnCounter = 0;

    List<Image> scrapClocks = new List<Image>(), workerClocks = new List<Image>();

    public override void Start()
    {
        base.Start();

        scrapMaker = this.transform.Find("ScrapMaker");
        workerMaker = this.transform.Find("WorkerMaker");

        foreach (Image clock in this.transform.Find("BU_UI/Scrap_Clocks").GetComponentsInChildren<Image>())
        {
            if (clock.name == "Clock")
            {
                scrapClocks.Add(clock);
            }
        }
        foreach (Image clock in this.transform.Find("BU_UI/Worker_Clocks").GetComponentsInChildren<Image>())
        {
            if (clock.name == "Clock")
            {
                workerClocks.Add(clock);
            }
        }

        requiredEnergy = 1;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (totalEnergy >= requiredEnergy)
        {
            if (state == true)
            {

                if (timeToSpawnCounter < timeToSpawnScrap)
                {
                    timeToSpawnCounter += Time.deltaTime;

                    ScrapClocks(timeToSpawnCounter / timeToSpawnScrap);
                }
                if (timeToSpawnCounter > timeToSpawnScrap)
                {
                    MakeScrap(totalEnergy);
                    timeToSpawnCounter = 0;
                }
            }
            else
            {

                if (timeToSpawnCounter < timeToSpawnWorker)
                {
                    timeToSpawnCounter += Time.deltaTime;

                    WorkerClocks(timeToSpawnCounter / timeToSpawnWorker);
                }
                if (timeToSpawnCounter > timeToSpawnWorker)
                {
                    MakeWorker(totalEnergy);
                    timeToSpawnCounter = 0;
                }
            }
        }
    }

    private void MakeScrap(int _totalEnergy)
    {
        //Should be a pool later on
        for (int i = 0; i < _totalEnergy; i++)
        {
            Instantiate(scrap, scrapMaker.transform.position, Quaternion.identity);
        }
    }
    private void MakeWorker(int _totalEnergy)
    {
        //Should be a pool later on
        for (int i = 0; i < _totalEnergy; i++)
        {
            Instantiate(worker, workerMaker.transform.position, Quaternion.identity);
        }
    }

    private void ScrapClocks(float _fillAmount)
    {
        for (int i = 0; i < totalEnergy; i++)
        {
            scrapClocks[i].fillAmount = _fillAmount;
        }
    }

    private void WorkerClocks(float _fillAmount)
    {
        for (int i = 0; i < totalEnergy; i++)
        {
            workerClocks[i].fillAmount = _fillAmount;
        }

    }
    public void State(bool _state)
    {
        state = _state;
        timeToSpawnCounter = 0;
        WorkerClocks(0);
        ScrapClocks(0);
    }

}
