using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporalCable : MonoBehaviour
{

    Transform cableEnd, cableStart, attachment;
    LineRenderer lineRenderer;
    BU_PowerPlant powerPlant;
    public bool energy;

    // Use this for initialization
    void Start()
    {
        cableStart = this.gameObject.transform.parent.transform;
        cableEnd = this.gameObject.transform.GetChild(0);
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, cableStart.transform.position);
        powerPlant = FindObjectOfType<BU_PowerPlant>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayRope();
    }

    //Display the rope with a line renderer
    private void DisplayRope()
    {
        lineRenderer.SetPosition(1, cableEnd.transform.position);
    }

}