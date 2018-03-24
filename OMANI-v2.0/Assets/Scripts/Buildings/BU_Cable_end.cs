using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cable_end : Interactible
{
    private temporalCable cable;
    private Transform lastParent;

    // Use this for initialization
    void Start()
    {
        cable = this.transform.parent.GetComponent<temporalCable>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cable.energy == true)
        {
            this.transform.tag = "Interactible";
        }
        else
        {
            this.transform.tag = "Untagged";
        }

    }

    public override void Action(BoyMovement boy)
    {
        if (boy.grabbedObject == null)
        {
            //Grabs
            boy.grabbedObject = this;
            lastParent = this.transform.parent;
            this.transform.SetParent(boy.hand.transform);
            this.transform.localPosition = Vector3.zero;
        }

        else
        {
            Collider[] objectsInArea = null;
            objectsInArea = Physics.OverlapSphere(transform.position, 2f, 1 << 14);

            float minDistance = 0;
            GameObject closest = null;

            //Checks if there are possible parents, like plugs.
            if (objectsInArea.Length > 1)
            {
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].name == "Plug")
                    {
                        float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                        if (minDistance == 0 || minDistance > distance)
                        {
                            minDistance = distance;
                            closest = objectsInArea[i].gameObject;
                        }
                    }
                }

                this.transform.SetParent(closest.transform);
                this.transform.localPosition = Vector3.zero;

                boy.grabbedObject = null;
            }
            else
            {
                this.transform.SetParent(lastParent);
                boy.grabbedObject = null;
            }
        }

    }
}
