using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public GameObject power { get; set; }
    public GameObject interactible { get; set; }
    LineRenderer lineRenderer;

    // Use this for initialization
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    public void Completed()
    {
        power = null;
        interactible = null;
        transform.gameObject.SetActive(false);
    }

    public void Failed()
    {
        power = null;
        interactible = null;
        transform.gameObject.SetActive(false);
    }

}
