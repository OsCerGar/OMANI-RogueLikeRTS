using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Resources_Workers : MonoBehaviour
{

    BU_Resources parentResources;

    // Use this for initialization
    void Start()
    {
        parentResources = this.transform.parent.GetComponent<BU_Resources>();
    }

    public bool StartWorker()
    {
        Debug.Log("Second Step");
        if (parentResources.MakeWorker())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
