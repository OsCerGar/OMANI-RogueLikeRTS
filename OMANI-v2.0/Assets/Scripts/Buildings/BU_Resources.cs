using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BU_Resources : BU_UniqueBuilding
{
    //State of the lever.
    public bool state = false;
    private Transform scrapMaker;
    private BU_Resources_Workers workerMaker;

    [SerializeField]
    private GameObject scrap, worker;

    [SerializeField]
    private float timeToSpawnScrap = 30, timeToSpawnWorker = 45;
    private float[] timeToSpawnWorkerCounter = new float[3];
    private float[] timeToSpawnScrapCounter = new float[3];

    private bool[] workersReady = new bool[3];

    List<Image> scrapClocks = new List<Image>(), workerClocks = new List<Image>();

    public override void Start()
    {
        base.Start();

        scrapMaker = this.transform.Find("ScrapMaker");
        workerMaker = this.transform.Find("WorkerMaker").GetComponent<BU_Resources_Workers>();

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

    //Makes Workers
    private void WorkerMaker()
    {
        //Checks energy up to 3 to see how much it creates. Sends info to the clocks with @WorkerClocks.
        if (totalEnergy > 0)
        {
            //Used to see how many workers are going to be build.
            int calcTotalEnergy = totalEnergy;

            for (int i = 0; i < workersReady.Length; i++)
            {
                if (calcTotalEnergy > 0 && workersReady[i] == false)
                {
                    if (timeToSpawnWorkerCounter[i] < timeToSpawnWorker)
                    {
                        timeToSpawnWorkerCounter[i] += Time.deltaTime;

                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWorker, i, Color.green);
                    }
                    if (timeToSpawnWorkerCounter[i] > timeToSpawnWorker)
                    {
                        workersReady[i] = true;
                        WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWorker, i, Color.cyan);
                    }
                    calcTotalEnergy -= 1;
                }
            }
        }

    }

    //Makes Scrap
    private void ScrapMaker()
    {
        if (totalEnergy > 0)
        {
            //Used to see how many workers are going to be build.
            int calcTotalEnergy = totalEnergy;

            for (int i = 0; i < workersReady.Length; i++)
            {
                if (calcTotalEnergy > 0)
                {
                    if (timeToSpawnScrapCounter[i] < timeToSpawnScrap)
                    {
                        timeToSpawnScrapCounter[i] += Time.deltaTime;

                        ScrapClocks(timeToSpawnScrapCounter[i] / timeToSpawnScrap, i);
                    }
                    if (timeToSpawnScrapCounter[i] > timeToSpawnScrap)
                    {
                        MakeScrap();
                        timeToSpawnScrapCounter[i] = 0;
                    }

                    calcTotalEnergy -= 1;
                }
            }
        }
    }


    private void MakeScrap()
    {
        //Should be a pool later on
        Instantiate(scrap, new Vector3(scrapMaker.transform.position.x + Random.Range(-1f, 1f), scrapMaker.transform.position.y, scrapMaker.transform.position.z), Quaternion.identity);
    }

    public bool MakeWorker()
    {
        bool spawned = false;
        for (int i = 0; i < 3; i++)
        {
            if (spawned == false && workersReady[i])
            {
                //Should be a pool later on
                Instantiate(worker, new Vector3(workerMaker.transform.position.x + Random.Range(-1f, 1f), workerMaker.transform.position.y, workerMaker.transform.position.z), Quaternion.identity);
                workersReady[i] = false;
                timeToSpawnWorkerCounter[i] = 0;
                WorkerClocks(timeToSpawnWorkerCounter[i] / timeToSpawnWorker, i, Color.green);
                spawned = true;
            }
        }

        return spawned;
    }

    private void ScrapClocks(float _fillAmount, int _clock)
    {

        scrapClocks[_clock].fillAmount = _fillAmount;

    }

    private void WorkerClocks(float _fillAmount, int _clock, Color _color)
    {

        workerClocks[_clock].fillAmount = _fillAmount;
        workerClocks[_clock].color = _color;

    }
    public void State(bool _state)
    {
        state = _state;
    }

}
