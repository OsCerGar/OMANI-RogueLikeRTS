using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BU : MonoBehaviour
{

    [HideInInspector]
    public SpriteRenderer redCircle, whiteCircle;

    public List<GameObject> workers = new List<GameObject>();
    public int numberOfWorkers = 0, maxnumberOfWorkers = 0;
    private GameObject workplace, door;
    public GameObject direction;


    private void Awake()
    {
        if (this.transform.Find("Office") != null)
        {
            door = this.transform.Find("Office").Find("Door").gameObject;
        }
        if (this.transform.Find("Direction") != null)
        {
            direction = this.transform.Find("Direction").gameObject;
        }
        workplace = this.transform.parent.transform.Find("Workplace").gameObject;

        redCircle = this.transform.Find("BU_UI/SelectionCircle").GetComponent<SpriteRenderer>();
        whiteCircle = this.transform.Find("BU_UI/SelectionCircleWhite").GetComponent<SpriteRenderer>();

    }

    public virtual void EnableCircle()
    {
        redCircle.enabled = true;

        //The sprite should be red.
        redCircle.material.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(5));

    }

    public virtual void EnableWhiteCircle()
    {
        whiteCircle.enabled = true;

        //The sprite should be red.
        whiteCircle.material.SetColor("_EmissionColor", Color.white * Mathf.LinearToGammaSpace(5));

    }

    public virtual void DisableCircle()
    {
        redCircle.enabled = false;
        whiteCircle.enabled = false;
    }

    public virtual void AddWorker(GameObject _worker)
    {
        if (numberOfWorkers < maxnumberOfWorkers)
        {
            workers.Add(_worker);
            _worker.GetComponent<NavMeshAgent>().Warp(workplace.transform.position);
        }
    }

    public virtual void RemoveWorker()
    {
        if (workers.Count != 0 && workers.Count < maxnumberOfWorkers)
        {
            workers[workers.Count - 1].GetComponent<NavMeshAgent>().Warp(door.transform.position);
            workers.Remove(workers[workers.Count - 1]);
        }
    }
}
