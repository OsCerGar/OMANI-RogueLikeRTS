using EZObjectPools;
using System.Collections.Generic;
using UnityEngine;
public class NumberPool : MonoBehaviour
{
    EZObjectPool damagenumber;
    GameObject Spawned; // list
    NumberScript text;
    Vector3 RandomPos;
    List<NumberScript> texts = new List<NumberScript>();
    Transform camera;
    // Start is called before the first frame update
    void Awake()
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

    public void NumberSpawn(Transform tr, float damage_value, Color _type, GameObject numberOwner, bool _restoring)
    {
        bool alreadyOwned = false;

        if (_restoring) { 
        foreach (NumberScript txt in texts)
        {
            if (txt.GetNumberOwner() == numberOwner)
            {
                txt.transform.position = numberOwner.transform.position;
               //txt.transform.LookAt(camera);
                txt.numberUpdate(damage_value, _type, _restoring);
                alreadyOwned = true;
            }

        }
        }
        if (alreadyOwned == false)
        {
            RandomPos = new Vector3(Random.Range(tr.position.x - 0.5f, tr.position.x + 0.5f), Random.Range(tr.position.y - 0.5f, tr.position.y + 0.5f), numberOwner.transform.position.z);

            damagenumber.TryGetNextObject(RandomPos, damagenumber.gameObject.transform.rotation, out Spawned);
            text = Spawned.transform.GetComponentInChildren<NumberScript>();
            text.SetNumberOwner(numberOwner);
            text.numberUpdate(damage_value, _type, _restoring);
            //PERFORMANCE MIS HUEVOS
            camera = Camera.main.transform;
            text.transform.forward = -camera.transform.forward;
            texts.Add(text);
        }
    }

    public void RemoveText(NumberScript _textToRemove)
    {
        texts.Remove(_textToRemove);
    }

}
