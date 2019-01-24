using EZObjectPools;
using UnityEngine;
using UnityEngine.UI;

public class NumberPool : MonoBehaviour
{
    EZObjectPool damagenumber;
    GameObject Spawned;

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

    public void NumberSpawn(Transform tr, int damage_value)
    {
        damagenumber.TryGetNextObject(tr.position, damagenumber.gameObject.transform.rotation, out Spawned);
        Spawned.transform.GetChild(0).GetComponentInChildren<Text>().text = damage_value.ToString();
    }

}
