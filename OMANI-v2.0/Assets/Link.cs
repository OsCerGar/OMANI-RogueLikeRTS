using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public GameObject power, interactible;
    private Vector3 point1, point2;
    LineRenderer lineRenderer;

    // Use this for initialization
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        point1 = Vector3.zero;
        point2 = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (power != null && interactible != null)
        {
            lineRenderer.SetPosition(0, power.transform.position);
            lineRenderer.SetPosition(1, interactible.transform.position);
        }
    }

    public void Completed()
    {
        point1 = Vector3.zero;
        point2 = Vector3.zero;
        transform.gameObject.SetActive(false);
    }

    public void Failed()
    {
        point1 = Vector3.zero;
        point2 = Vector3.zero;
        transform.gameObject.SetActive(false);
    }

}
