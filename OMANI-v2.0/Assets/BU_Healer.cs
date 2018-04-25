using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BU_Healer : BU_UniqueBuilding
{
    Transform healer;
    Transform[] healPositions;
    int healing;
    float timeToHeal = 0, totalTimeToHeal = 5;
    BU_Healing_GUI healingGUI;

    float healingSize = 6;

    //Audios

    AudioSource addedWorker;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        maxnumberOfWorkers = 8;
        requiredEnergy = 2;

        //Positions where the workers will go
        healPositions = this.transform.Find("Positions").GetComponentsInChildren<Transform>();

        healingGUI = this.transform.GetComponentInChildren<BU_Healing_GUI>();

        healer = this.transform.Find("Healer");

        //Audio
        addedWorker = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        numberOfWorkers = workers.Count;

        switch (totalEnergy)
        {
            case 2:
                healing = 1;
                break;
            case 3:
                healing = 2;
                break;
            case 4:
                healing = 3;
                break;
        }

        if (totalEnergy >= requiredEnergy)
        {

            timeToHeal += Time.deltaTime;

            healingGUI.ChangeHealingClock(timeToHeal / totalTimeToHeal);

            if (timeToHeal > totalTimeToHeal)
            {
                Healer(healing);
                timeToHeal = 0;
            }
        }

        else
        {


            healingGUI.ChangeHealingClock(0);
        }
    }

    public override void AddWorker(GameObject _worker)
    {
        if (numberOfWorkers < maxnumberOfWorkers)
        {
            _worker.GetComponent<NavMeshAgent>().Warp(healPositions[numberOfWorkers].transform.position);
            workers.Add(_worker);
            addedWorker.Play();
        }
    }

    void Healer(int _heal)
    {

        Collider[] objectsInArea = null;
        objectsInArea = Physics.OverlapSphere(transform.position, healingSize, 1 << 9);

        //Checks if there are possible interactions.
        if (objectsInArea.Length > 0)
        {

            for (int i = 0; i < objectsInArea.Length; i++)
            {
                if (objectsInArea[i].GetComponent<NPC>() != null)
                {
                    objectsInArea[i].GetComponent<NPC>().Heal(_heal);
                }
            }

        }

    }


}
