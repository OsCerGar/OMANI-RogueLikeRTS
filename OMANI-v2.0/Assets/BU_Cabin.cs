using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Cabin : MonoBehaviour
{

    public bool workerInside = false;
    [SerializeField]
    public GameObject UI;
    public GameObject direction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GUI_Enabled()
    {
        UI.SetActive(true);
    }

    public void GUI_Disabled()
    {
        UI.SetActive(false);
    }
}
