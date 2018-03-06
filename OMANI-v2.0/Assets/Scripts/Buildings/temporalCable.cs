using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporalCable : MonoBehaviour
{
    [SerializeField]
    Transform cableEnd, cableStart;
    LineRenderer lineRenderer;
    BU_PowerPlant powerPlant;
    public bool energy;
    Color32 lineMaterial;

    // Use this for initialization
    void Start()
    {
        if (cableStart == null)
        {
            cableStart = this.gameObject.transform.parent.transform;
        }
        if (cableEnd == null)
        {

            cableEnd = this.gameObject.transform.GetChild(0);
        }

        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        //Adding heigh because the elevator starts at the ground
        lineRenderer.SetPosition(0, cableStart.transform.position);
        powerPlant = FindObjectOfType<BU_PowerPlant>();
        lineMaterial = lineRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayRope();

        if (energy)
        {
            lineRenderer.material.color = Color.yellow;

        }
        else
        {
            //lineRenderer.material = lineMaterial;
            lineRenderer.material.color = lineMaterial;

        }
    }

    //Display the rope with a line renderer
    private void DisplayRope()
    {
        lineRenderer.SetPosition(1, cableEnd.transform.position);
    }

}