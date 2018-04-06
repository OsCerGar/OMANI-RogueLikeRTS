using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Equipment_Swordsmith : Interactible
{
    [SerializeField]
    GameObject swordman;

    public override void Action(BoyMovement _boy)
    {
        if (_boy.grabbedObject == null)
        {
            disableRigid();
            //Grabs
            _boy.grabbedObject = this;
            this.transform.SetParent(_boy.hand.transform);
            this.transform.localPosition = Vector3.zero;
        }

        // If you presss action while there is a nearby barroboy.
        else
        {

            Collider[] objectsInArea = null;
            objectsInArea = Physics.OverlapSphere(transform.position, 2f, 1 << 9);

            float minDistance = 0;
            Worker closest = null;

            //Checks if there are possible interactions.
            if (objectsInArea.Length > 1)
            {

                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    //If you want it to work with every NPC just change the GetComponent to NPC
                    if (objectsInArea[i].GetComponent<Worker>() != null)
                    {
                        float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                        if (minDistance == 0 || minDistance > distance)
                        {
                            minDistance = distance;
                            closest = objectsInArea[i].GetComponent<Worker>();
                        }
                    }
                }


                if (closest != null)
                {

                    //Changes the Worker to the type of NPC this is.
                    closest.Mutate(swordman);

                    _boy.grabbedObject = null;

                    //Destroys itself
                    Destroy(this.gameObject);
                }

                else
                {
                    enableRigid();
                    this.transform.SetParent(null);
                    _boy.grabbedObject = null;
                }
            }
        }

    }

}
