using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Type_Archer : Interactible
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
            objectsInArea = Physics.OverlapSphere(transform.position, 1f, 1 << 15);

            BU_WeaponsBay closest = null;

            //Checks if there are possible interactions.
            if (objectsInArea.Length > 1)
            {
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].GetComponentInParent<BU_WeaponsBay>().buildingTypeAndBehaviour == null)
                    {

                        closest = objectsInArea[i].GetComponentInParent<BU_WeaponsBay>();
                    }

                }

                if (closest != null)
                {

                    //Changes the Building type to whatever
                    closest.gameObject.AddComponent(typeof(BU_Archer));
                    closest.GetComponent<BU_Archer>().equipmentToSpawn = buildingEquipment;

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
