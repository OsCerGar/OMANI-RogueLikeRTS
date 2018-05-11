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

    public float timeToSpawnScrap = 30, timeToSpawnWorker = 45;
    private float[] timeToSpawnCounter = new float[3];

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
            // Makes Scrap
            if (state == true)
            {
                ScrapMaker();
            }

            // Makes Clocks
            else
            {
                WorkerMaker();
            }
        }
    }

    private void WorkerMaker()
    {

        if (totalEnergy > 0)
        {
            if (timeToSpawnCounter[0] < timeToSpawnWorker)
            {
                timeToSpawnCounter[0] += Time.deltaTime;

                WorkerClocks(timeToSpawnCounter[0] / timeToSpawnWorker, 0);
            }
            if (timeToSpawnCounter[0] > timeToSpawnWorker)
            {
                MakeWorker();
                timeToSpawnCounter[0] = 0;
            }
        }
        if (totalEnergy > 1)
        {
            if (timeToSpawnCounter[1] < timeToSpawnWorker)
            {
                timeToSpawnCounter[1] += Time.deltaTime;

                WorkerClocks(timeToSpawnCounter[1] / timeToSpawnWorker, 1);
            }
            if (timeToSpawnCounter[1] > timeToSpawnWorker)
            {
                MakeWorker();
                timeToSpawnCounter[1] = 0;
            }
        }
        if (totalEnergy > 2)
        {
            if (timeToSpawnCounter[2] < timeToSpawnWorker)
            {
                timeToSpawnCounter[2] += Time.deltaTime;

                WorkerClocks(timeToSpawnCounter[2] / timeToSpawnWorker, 2);
            }
            if (timeToSpawnCounter[2] > timeToSpawnWorker)
            {
                MakeWorker();
                timeToSpawnCounter[2] = 0;
            }

        }
    }

    private void ScrapMaker()
    {
        if (totalEnergy > 0)
        {
            if (timeToSpawnCounter[0] < timeToSpawnScrap)
            {
                timeToSpawnCounter[0] += Time.deltaTime;

                ScrapClocks(timeToSpawnCounter[0] / timeToSpawnScrap, 0);
            }
            if (timeToSpawnCounter[0] > timeToSpawnScrap)
            {
                MakeScrap();
                timeToSpawnCounter[0] = 0;
            }
        }
        if (totalEnergy > 1)
        {
            if (timeToSpawnCounter[1] < timeToSpawnScrap)
            {
                timeToSpawnCounter[1] += Time.deltaTime;

                ScrapClocks(timeToSpawnCounter[1] / timeToSpawnScrap, 1);
            }
            if (timeToSpawnCounter[1] > timeToSpawnScrap)
            {
                MakeScrap();
                timeToSpawnCounter[1] = 0;
            }
        }
        if (totalEnergy > 2)
        {
            if (timeToSpawnCounter[2] < timeToSpawnScrap)
            {
                timeToSpawnCounter[2] += Time.deltaTime;

                ScrapClocks(timeToSpawnCounter[2] / timeToSpawnScrap, 2);
            }
            if (timeToSpawnCounter[2] > timeToSpawnScrap)
            {
                MakeScrap();
                timeToSpawnCounter[2] = 0;
            }
        }
    }


    private void MakeScrap()
    {
        //Should be a pool later on
        Instantiate(scrap, scrapMaker.transform.position, Quaternion.identity);

    }
    private void MakeWorker()
    {
        //Should be a pool later on
        Instantiate(worker, workerMaker.transform.position, Quaternion.identity);

    }

    private void ScrapClocks(float _fillAmount, int _clock)
    {

        scrapClocks[_clock].fillAmount = _fillAmount;

    }

    private void WorkerClocks(float _fillAmount, int _clock)
    {

        workerClocks[_clock].fillAmount = _fillAmount;


    }
    public void State(bool _state)
    {
        state = _state;
        for (int i = 0; i < timeToSpawnCounter.Length; i++)
        {
            timeToSpawnCounter[i] = 0;
        }

        for (int i = 0; i < 3; i++)
        {
            WorkerClocks(0, i);
        }
        for (int i = 0; i < 3; i++)
        {
            ScrapClocks(0, i);
        }
    }

}
