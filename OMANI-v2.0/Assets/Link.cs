using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    [SerializeField]
    public GameObject power { get; set; }
    public GameObject interactible { get; set; }
    Electric electricity;

    // Use this for initialization
    void Awake()
    {
        electricity = GetComponent<Electric>();
    }

    public void visualLink()
    {
        if (power != null)
            electricity.transformPointA = power.transform;
        if (interactible != null)
            electricity.transformPointB = interactible.transform;
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
