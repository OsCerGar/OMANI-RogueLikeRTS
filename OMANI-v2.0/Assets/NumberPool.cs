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

    public void NumberSpawn(Transform tr, float damage_value, Color _type)
    {
        damagenumber.TryGetNextObject(tr.position, damagenumber.gameObject.transform.rotation, out Spawned);
        Text text = Spawned.transform.GetChild(0).GetComponentInChildren<Text>();

        text.text = damage_value.ToString();
        text.color = _type;
    }

}
