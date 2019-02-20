using EZObjectPools;
using UnityEngine;

public class NumberPool : MonoBehaviour
{
    EZObjectPool damagenumber;
    GameObject Spawned; // list
    NumberScript text;
    // Start is called before the first frame update
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "DamageNumber")
            {
                damagenumber = item;
            }
        }

    }

    public void NumberSpawn(Transform tr, float damage_value, Color _type, GameObject numberOwner)
    {

        if (Spawned == null || !Spawned.activeSelf)
        {
            damagenumber.TryGetNextObject(tr.position, damagenumber.gameObject.transform.rotation, out Spawned);
            text = Spawned.transform.GetComponentInChildren<NumberScript>();
            text.SetNumberOwner(numberOwner);

        }
        else
        {
            if (text.GetNumberOwner() == numberOwner)
            {
                text.numberUpdate(damage_value, _type);
            }
            else
            {
                //spawns another number
            }
        }
    }

}
