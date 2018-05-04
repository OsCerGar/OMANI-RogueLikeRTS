using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cable_end : Interactible
{
    private CableComponent cable;
    private Transform lastParent;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        disableRigid();

        cable = this.transform.parent.GetComponent<CableComponent>();
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
            cable.CableLength(0);
            this.transform.tag = "Untagged";
        }

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

            if (this.transform.parent != null && this.transform.parent.GetComponent<BU_Plug>())
            {

                this.transform.parent.GetComponent<BU_Plug>().ChangeColor(Color.white);
            }

            //Grabs
            _boy.grabbedObject.Add(this);
            lastParent = this.transform.parent;
            this.transform.SetParent(_boy.hand.transform);
            this.transform.localPosition = Vector3.zero;

        }

        else
        {
            Collider[] objectsInArea = null;
            objectsInArea = Physics.OverlapSphere(transform.position, 3f, 1 << 14);

            float minDistance = 0;
            GameObject closest = null;

            //Checks if there are possible parents, like plugs.
            if (objectsInArea.Length > 1)
            {
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].GetComponent<BU_Plug>() != null && objectsInArea[i].transform.childCount < 1)
                    {
                        float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                        if (minDistance == 0 || minDistance > distance)
                        {
                            minDistance = distance;
                            closest = objectsInArea[i].gameObject;
                        }
                    }
                }

                if (closest != null)
                {
                    disableRigid();
                    cable.CableLength(16);

                    this.transform.SetParent(closest.transform);
                    this.transform.localPosition = Vector3.zero;

                    _boy.grabbedObject.Remove(this);


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
