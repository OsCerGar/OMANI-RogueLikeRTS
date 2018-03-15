using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Plug : MonoBehaviour
{

    MeshRenderer mesh;
    bool energy;
    // Use this for initialization
    void Start()
    {
        mesh = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount > 0)
        {
            if (energy == false)
            {
                if (this.transform.GetChild(0).GetComponent<B_Cable_end>().cable.energy == true)
                {
                    energy = true;
                    mesh.material.color = Color.yellow;
                }
            }

            else
            {
                energy = false;
                mesh.material.color = Color.white;
            }

        }
    }


}
