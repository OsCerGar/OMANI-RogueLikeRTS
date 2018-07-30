using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cabin : MonoBehaviour
{

    public bool workerInside { get; set; }
    public bool ready { get; set; }
    public bool alreadyReady;
    private float uiTimer;
    public GameObject UI { get; set; }
    public GameObject direction { get; set; }
    private PeoplePool peoplePool;
    private Animator doorAnimation;

    // Use this for initialization
    void Start()
    {
        peoplePool = FindObjectOfType<PeoplePool>();
        doorAnimation = this.transform.GetComponent<Animator>();
        direction = this.transform.Find("Direction").gameObject;
        UI = this.transform.Find("OrderDirection").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (workerInside) { GUI_Disabled(); }
        if (!ready) { GUI_Disabled(); }
        if (Time.time - uiTimer > 0.05f)
        {
            GUI_Disabled();
        }
    }

    public void CabinReady()
    {
        if (!alreadyReady)
        {
            ready = true;
            doorAnimation.SetBool("Open", true);
            //Anim play
            alreadyReady = true;
        }
    }

    public void CabinNotReady()
    {
        ready = false;
        doorAnimation.SetBool("Open", false);
    }

    public void GUI_Enabled()
    {
        if (ready)
        {
            uiTimer = Time.time;
            UI.SetActive(true);
        }
    }

    public void GUI_Disabled()
    {
        UI.SetActive(false);
    }

    public virtual void AddWorker(NPC _worker)
    {
        if (!workerInside && _worker.boyType == "Worker")
        {
            _worker.AI_SetTarget(null);
            _worker.gameObject.SetActive(false);
            CabinNotReady();
            workerInside = true;
        }
    }

    public virtual void TurnWorker()
    {
        peoplePool.SpearWarriorSpawn(direction.transform);
        workerInside = false;
        alreadyReady = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People") && other.tag != "Player")
        {
            if (!workerInside && ready)
            {
                NPC worker = other.GetComponent<NPC>();
                if (worker.AI_GetTarget() == this.direction)
                {
                    AddWorker(worker);
                }
            }
        }
    }

}
