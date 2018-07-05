using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cable_end : Interactible
{
    private CableComponent cable;
    private SpringJoint spring;
    private Transform lastParent;
    private bool collecting = false, collectingStarters = false;
    private float maxDistance;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        disableRigid();

        cable = this.transform.parent.GetComponent<CableComponent>();
        spring = this.transform.parent.GetComponent<SpringJoint>();

        maxDistance = spring.maxDistance;
    }

    // Update is called once per frame
    void Update()
    {

        if (cable.energy == true)
        {
            this.transform.tag = "Interactible";
        }
        // Disengage from whatever was attached.
        else
        {
            cable.CableLength(0);
            this.transform.tag = "Untagged";

            if (this.transform.parent != null && this.transform.parent != cable.transform)
            {
                if (this.transform.root.CompareTag("Player"))
                {
                    this.transform.root.GetComponent<BoyMovement>().grabbedObject.Remove(this);
                }

                if (this.transform.parent != null && this.transform.parent.GetComponent<BU_Plug>())
                {
                    this.transform.parent.GetComponent<BU_Plug>().ChangeColor(Color.white);
                }

                enableRigid();
                this.transform.SetParent(null);
                collecting = true;
            }
        }

        //Starts Collecting
        if (collecting == true)
        {

            if (collectingStarters == false)
            {
                //This calcs starting distance between cable end and wanted position.
                maxDistance = Vector3.Distance(this.transform.position, cable.transform.position);
                spring.maxDistance = maxDistance;
                collectingStarters = true;

            }

            float distRatio = (maxDistance) / (Vector3.Distance(this.transform.position, cable.transform.position));

            // If its close to the point, it Lerps itself instead of using physics to avoid weird behaviours.
            if (distRatio > 0.2f)
            {
                spring.maxDistance -= (distRatio * 0.075f) + 0.05f;
            }

            float distance = Vector3.Distance(this.transform.position, cable.transform.position);

            if (distance < 4f)
            {
                disableRigid();
                this.transform.position = Vector3.Lerp(this.transform.position, cable.transform.position, 0.4f);
            }

            if (distance < 2.5f)
            {
                this.transform.position = cable.transform.position;
                this.transform.parent = cable.transform;
                collecting = false;
                spring.maxDistance = maxDistance;
            }
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
                    cable.CableLength(13);

                    this.transform.SetParent(closest.transform);
                    this.transform.localPosition = Vector3.zero;

                    _boy.grabbedObject.Remove(this);


                }

            }

            else
            {
                enableRigid();
                cable.CableLength(2);
                collecting = true;
                this.transform.SetParent(null);
                _boy.grabbedObject.Remove(this);
            }
        }

    }
    */
}
