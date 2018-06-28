using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public List<Interactible> inventory = new List<Interactible>();
    LayerMask finalLayer;
    Player player;
    Powers power;

    void Start() { power = FindObjectOfType<Powers>(); }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 1"))
            Action();
    }

    private void Action()
    {
        if (inventory.Count < 1)
        {
            Collider[] objectsInArea = null;
            objectsInArea = Physics.OverlapSphere(transform.position, 2f, finalLayer);

            float minDistance = 0;
            GameObject closest = null;
            Robot robot = null;
            bool alreadyGrabbedObject = false;

            // Checks if there are interactible objects nearby
            if (objectsInArea.Length < 1)
            {
                //Roll();
            }

            else
            {
                //If there are, the closest one.
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].tag == "Interactible")
                    {
                        foreach (Interactible interact in inventory)
                        {
                            if (interact.gameObject == objectsInArea[i].gameObject)
                            {
                                alreadyGrabbedObject = true;
                            }
                        }

                        if (alreadyGrabbedObject == false)
                        {
                            float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                            if (minDistance == 0 || minDistance > distance)
                            {
                                minDistance = distance;
                                closest = objectsInArea[i].gameObject;
                            }
                        }
                    }
                    else if (objectsInArea[i].tag == "People")
                    {
                        float distance = Vector3.Distance(objectsInArea[i].transform.position, this.gameObject.transform.position);

                        if (minDistance == 0 || minDistance > distance)
                        {
                            minDistance = distance;
                            robot = objectsInArea[i].GetComponent<Robot>();
                        }
                    }
                }
            }

            if (robot != null)
            {
                robot.StartResurrection();
            }

            if (closest != null)
            {
                //Does whatever Action does
                //Sends himself to the action manager
                //closest.GetComponent<Interactible>().Action(this);
            }
            else if (inventory.Count > 0)
            {
                //inventory[inventory.Count - 1].Action(this);

            }
        }

        else
        {
            //inventory[inventory.Count - 1].Action(this);
        }
    }


}
