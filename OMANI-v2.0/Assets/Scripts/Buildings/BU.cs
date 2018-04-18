using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU : MonoBehaviour
{

    [HideInInspector]
    public SpriteRenderer circle;

    private void Awake()
    {
        circle = this.transform.Find("BU_UI/SelectionCircle").GetComponent<SpriteRenderer>();
    }

    public virtual void EnableCircle()
    {
        circle.enabled = true;

        //The sprite should be red.
        circle.material.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(50));

    }

    public virtual void DisableCircle()
    {
        circle.enabled = false;
    }
}
