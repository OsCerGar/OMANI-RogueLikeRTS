using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Type : Interactible
{

    [SerializeField]
    GameObject buildingEquipment;

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
            objectsInArea = Physics.OverlapSphere(transform.position, 3f, 1 << 15);

            BU_EquipmentBuildings closest = null;

            //Checks if there are possible interactions.
            if (objectsInArea.Length > 1)
            {
                BU_EquipmentBuildings bu;
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    bu = objectsInArea[i].GetComponentInParent<BU_EquipmentBuildings>();
                    if (bu.buildingTypeAndBehaviour == null)
                    {

                        closest = bu;
                    }

                }

                if (closest != null)
                {

                    //Changes the Building type to whatever
                    closest.gameObject.AddComponent(typeof(BU_BuildingType));
                    closest.GetComponent<BU_BuildingType>().equipmentToSpawn = buildingEquipment;

                    _boy.grabbedObject = null;

                    disableRigid();

                    this.transform.SetParent(null);
                    _boy.grabbedObject = null;

                    //Destroys itself
                    this.transform.parent = closest.gameObject.transform.Find("BU_UI").Find("BU_Type");
                    this.transform.localPosition = Vector3.zero;
                    this.transform.rotation = closest.gameObject.transform.Find("BU_UI").Find("BU_Type").transform.rotation;

                }

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
