using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BU_Teleport : BU_UniqueBuilding
{
    private float timeToTeleport = 5, sizeToTeleport = 7;
    private GameObject gUI_Teleport;
    private BU_Teleport_Interactible teleport;

    private Vector3 teleportPosition;

    public bool teleporting;


    // Use this for initialization
    public override void Start()
    {
        base.Start();
        notOnlyWorkers = true;
        maxnumberOfWorkers = 0;
        requiredEnergy = 1;
        gUI_Teleport = this.transform.Find("GUI_Teleport").gameObject;

        teleport = this.transform.Find("Teleport").GetComponent<BU_Teleport_Interactible>();

        teleportPosition = teleport.transform.position;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (totalEnergy >= requiredEnergy)
        {
            teleport.energy = true;
        }

        else
        {
            teleport.energy = false;
        }


        if (teleporting == true)
        {
            timeToTeleport += Time.deltaTime;
            gUI_Teleport.SetActive(true);
            gUI_Teleport.transform.Rotate(Vector3.up * Time.deltaTime * 5, Space.World);

            if (timeToTeleport > 5)
            {
                timeToTeleport = 0;
                teleporting = false;
                LoadingTeleport();
            }

        }
        else
        {
            gUI_Teleport.SetActive(false);
            timeToTeleport = 0;
        }

    }

    // If the teleport is planted teleporting  == true;
    public void StartTeleport()
    {
        if (teleport.planted == true && totalEnergy >= requiredEnergy)
        {
            teleporting = true;
        }

    }
    private void LoadingTeleport()
    {
        Collider[] objectsInArea = null;
        objectsInArea = Physics.OverlapSphere(transform.position, sizeToTeleport, 1 << 9);

        List<NavMeshAgent> peoples = new List<NavMeshAgent>();
        GameObject player = null;
        //Checks if there are possible interactions.
        if (objectsInArea.Length > 1)
        {
            {
                NavMeshAgent people;
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].tag == "Player")
                    {
                        player = objectsInArea[i].gameObject;
                    }

                    else
                    {
                        people = objectsInArea[i].GetComponent<NavMeshAgent>();

                        if (people != null)
                        {
                            peoples.Add(people);
                        }
                    }
                }
            }

            teleport.Teleport(peoples, player);

        }
    }


    public void Teleport(List<NavMeshAgent> _people, GameObject _barroboy)
    {
        foreach (NavMeshAgent people in _people)
        {
            people.Warp(new Vector3(this.transform.position.x + Random.Range(2, sizeToTeleport), this.transform.position.y, this.transform.position.z + Random.Range(2, sizeToTeleport)));
        }

        Vector3 randomPos = new Vector3(this.transform.position.x + Random.Range(2, sizeToTeleport), this.transform.position.y, this.transform.position.z + Random.Range(2, sizeToTeleport));
        _barroboy.transform.position = randomPos;
        teleport.transform.position = teleportPosition;
    }
}
