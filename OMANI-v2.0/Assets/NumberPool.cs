using EZObjectPools;
using System.Collections.Generic;
using UnityEngine;
public class NumberPool : MonoBehaviour
{
    EZObjectPool damagenumber;
    GameObject Spawned; // list
    NumberScript text;

    List<NumberScript> texts = new List<NumberScript>();
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
        bool alreadyOwned = false;
        foreach (NumberScript txt in texts)
        {
            if (txt.GetNumberOwner() == numberOwner)
            {
                text.numberUpdate(damage_value, _type);
                alreadyOwned = true;
            }
        }
        if (alreadyOwned == false)
        {
            damagenumber.TryGetNextObject(tr.position, damagenumber.gameObject.transform.rotation, out Spawned);
            text = Spawned.transform.GetComponentInChildren<NumberScript>();
            text.SetNumberOwner(numberOwner);
            text.numberUpdate(damage_value, _type);

            texts.Add(text);
        }
    }

    public void RemoveText(NumberScript _textToRemove)
    {
        texts.Remove(_textToRemove);
    }

}
