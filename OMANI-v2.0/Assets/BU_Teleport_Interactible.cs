using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BU_Teleport_Interactible : Interactible
{

    public bool energy, planted, teleporting;
    private float timeToTeleport = 0, sizeToTeleport = 7;
    private GameObject gUI_Teleport, energyLight, plantedLight;
    private BU_Teleport bu_Teleport;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        energyLight = this.transform.Find("Energy").gameObject;
        plantedLight = this.transform.Find("Planted").gameObject;
        gUI_Teleport = this.transform.Find("GUI_Teleport").gameObject;
        bu_Teleport = this.transform.parent.GetComponent<BU_Teleport>();
        disableRigid();
    }

    void Update()
    {
        if (energy == true)
        {
            energyLight.SetActive(true);
        }

        else
        {
            energyLight.SetActive(false);
            planted = false;
        }

        if (planted == true)
        {
            plantedLight.SetActive(true);
        }

        else
        {
            plantedLight.SetActive(false);
        }

        if (teleporting == true)
        {
            timeToTeleport += Time.deltaTime;
            gUI_Teleport.SetActive(true);
            gUI_Teleport.transform.Rotate(Vector3.up * Time.deltaTime * 5, Space.World);

            if (timeToTeleport > 5)
            {
                timeToTeleport = 0;
                planted = false;
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

            bu_Teleport.Teleport(peoples, player);

        }
    }
    /*
    public override void Action(BoyMovement _boy)
    {
        bool alreadyGrabbedObject = false;

        foreach (Interactible interact in _boy.grabbedObject)
        {
            if (interact.gameObject == this.gameObject)
            {
                alreadyGrabbedObject = true;
            }
        }
        if (energy == true && planted == true)
        {
            teleporting = true;
        }

        else
        {
            if (alreadyGrabbedObject == false && _boy.grabbedObject.Count < 3)
            {
                planted = false;
                disableRigid();

                //Grabs
                _boy.grabbedObject.Add(this);
                this.transform.SetParent(_boy.hand.transform);
                this.transform.localPosition = Vector3.zero;
            }

            // If you presss action while there is a nearby object.
            else
            {
                this.transform.SetParent(null);
                _boy.grabbedObject.Remove(this);
                this.transform.rotation = Quaternion.identity;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f, this.transform.position.z);
                planted = true;
            }
        }
    }
    */
    public void Teleport(List<NavMeshAgent> _people, GameObject _barroboy)
    {
        foreach (NavMeshAgent people in _people)
        {
            people.Warp(new Vector3(this.transform.position.x + Random.Range(2, sizeToTeleport), this.transform.position.y, this.transform.position.z + Random.Range(2, sizeToTeleport)));
        }

        _barroboy.transform.position = new Vector3(this.transform.position.x + Random.Range(2, sizeToTeleport), this.transform.position.y, this.transform.position.z + Random.Range(2, sizeToTeleport));
    }

}


