using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    MapManager mManager;
    SavageCamp camp;
	// Use this for initialization
	void Start () {
        camp = transform.parent.GetComponent<SavageCamp>();
        mManager = FindObjectOfType<MapManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<ShepHerd>() != null)
            {
                if (other.GetComponent<ShepHerd>().Obj != null)
                {
                    other.GetComponent<ShepHerd>().SetFree();
                    mManager.Res.Remove(other.GetComponent<ShepHerd>().Obj);

                    other.GetComponent<ShepHerd>().DropObj();

                    camp.createSavageShack();
                    camp.someoneSearching = false;
                }
                
            }
        }
    }
}
