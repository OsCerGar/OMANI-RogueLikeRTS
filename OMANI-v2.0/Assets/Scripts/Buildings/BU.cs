using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BU : MonoBehaviour
{

    [HideInInspector]
    public SpriteRenderer redCircle, whiteCircle;
    public bool notOnlyWorkers = false;
    public List<NPC> workers = new List<NPC>();
    public int numberOfWorkers = 0, maxnumberOfWorkers = 0;
    public GameObject door;
    public GameObject direction;



    private void Awake()
    {
        if (this.transform.Find("Door") != null)
        {
            door = this.transform.Find("Door").gameObject;
        }
        if (this.transform.Find("Direction") != null)
        {
            direction = this.transform.Find("Direction").gameObject;
        }


        if (this.transform.Find("BU_UI/SelectionCircle") != null)
        {
            redCircle = this.transform.Find("BU_UI/SelectionCircle").GetComponent<SpriteRenderer>();
        }
        if (this.transform.Find("BU_UI/SelectionCircleWhite") != null)
        {
            whiteCircle = this.transform.Find("BU_UI/SelectionCircleWhite").GetComponent<SpriteRenderer>();
        }
    }

    public virtual void EnableCircle()
    {
        if (redCircle != null)
        {
            redCircle.enabled = true;
            //The sprite should be red.
            redCircle.material.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(5));

        }


    }

    public virtual void EnableWhiteCircle()
    {
        if (whiteCircle != null)
        {

            whiteCircle.enabled = true;
            //The sprite should be red.
            whiteCircle.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace(5));

        }


    }

    public virtual void DisableCircle()
    {
        if (redCircle != null)
        {
            redCircle.enabled = false;
        }
        if (whiteCircle != null)
        {

            whiteCircle.enabled = false;
        }
    }

    public virtual void AddWorker(NPC _worker)
    {
        if (numberOfWorkers < maxnumberOfWorkers && _worker.boyType == "Worker")
        {
            workers.Add(_worker);
            _worker.AI_SetTarget(null);
            _worker.gameObject.SetActive(false);
        }
    }

    public virtual void RemoveWorker()
    {
        if (workers.Count != 0 && workers.Count <= maxnumberOfWorkers)
        {
            NPC worker = workers[workers.Count - 1];

            worker.gameObject.GetComponent<NavMeshAgent>().Warp(door.transform.position);
            worker.AI_SetTarget(door);

            worker.gameObject.SetActive(true);
            workers.Remove(worker);

        }
    }
}
