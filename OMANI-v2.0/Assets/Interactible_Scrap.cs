using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible_Scrap : Interactible
{
    [SerializeField]
    private int heal;

    int build, mask, peopl;
    public override void Start()
    {
        base.Start();
        build = 1 << LayerMask.NameToLayer("Building");
        peopl = 1 << LayerMask.NameToLayer("People");
        mask = build | peopl;

    }
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


        if (alreadyGrabbedObject == false && _boy.grabbedObject.Count < 3)
        {
            disableRigid();

            //Grabs
            _boy.grabbedObject.Add(this);
            this.transform.SetParent(_boy.hand.transform);
            this.transform.localPosition = Vector3.zero;
        }

        // If you presss action while there is a nearby barroboy.
        else
        {

            Collider[] objectsInArea = null;
            objectsInArea = Physics.OverlapSphere(transform.position, 2f, mask);
            float minDistance = 0;
            GameObject closest = null;

            //Checks if there are possible interactions.
            if (objectsInArea.Length > 1)
            {

                for (int i = 0; i < objectsInArea.Length; i++)
                {

                    if (objectsInArea[i].transform.root == objectsInArea[i].transform || objectsInArea[i].transform.CompareTag("Building"))
                    {

                        if (!objectsInArea[i].GetComponent<Player>())
                        {

                            float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                            if (minDistance == 0 || minDistance > distance)
                            {
                                minDistance = distance;
                                closest = objectsInArea[i].gameObject;
                            }
                        }
                    }
                }

                if (closest != null)
                {
                    if (closest.CompareTag("Building"))
                    {
                        BU_ScrapBox scrapbox = closest.GetComponent<BU_ScrapBox>();
                        BU_Resources_Workers resourcesWorker = closest.GetComponent<BU_Resources_Workers>();

                        if (scrapbox != null)
                        {
                            scrapbox.SpitEquipment();

                            disableRigid();

                            this.transform.SetParent(null);
                            _boy.grabbedObject.Remove(this);

                            //Should be pooled.
                            Destroy(this.gameObject);

                        }

                        if (resourcesWorker != null)
                        {
                            Debug.Log("First Scrap step");
                            if (resourcesWorker.StartWorker())
                            {
                                disableRigid();

                                this.transform.SetParent(null);
                                _boy.grabbedObject.Remove(this);

                                //Should be pooled.
                                Destroy(this.gameObject);
                            }
                        }


                    }

                    else
                    {
                        NPC npc = closest.GetComponent<NPC>();
                        if (npc.life != npc.startLife)
                        {
                            npc.Heal(heal);

                            disableRigid();

                            this.transform.SetParent(null);
                            _boy.grabbedObject.Remove(this);

                            //Should be pooled.
                            Destroy(this.gameObject);

                        }
                    }
                }

                else
                {
                    enableRigid();

                    this.transform.SetParent(null);
                    _boy.grabbedObject.Remove(this);
                }


            }

        }

    }
}
