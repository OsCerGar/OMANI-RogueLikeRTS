using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_PlugUpDown : MonoBehaviour
{

    [SerializeField]
    GameObject Plug;
    [SerializeField]
    Vector3 start, end;
    [SerializeField]
    float height;
    bool playerClose = false;


    // Use this for initialization
    void Start()
    {
        Plug = this.gameObject.transform.GetChild(0).gameObject;
        start = Plug.transform.position;
        end = Plug.transform.position + new Vector3(0, height, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerClose)
        {
            Plug.transform.position = Vector3.Lerp(Plug.transform.position, start, 0.1f);
        }
        else
        {
            Plug.transform.position = Vector3.Lerp(Plug.transform.position, end, 0.1f);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))
        {
            playerClose = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("People"))
        {
            playerClose = false;

        }
    }

}
