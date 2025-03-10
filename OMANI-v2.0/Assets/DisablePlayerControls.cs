﻿using System.Collections.Generic;
using UnityEngine;
public class DisablePlayerControls : MonoBehaviour
{

    public CharacterMovement movement;
    public PointerEnabler pointer;
    public Powers powers;
    float originalspeed;

    public List<Canvas> canvas;

    [SerializeField]
    GameObject extras;

    [SerializeField]
    bool alsoMovement;

    private void OnEnable()
    {
        pointer = FindObjectOfType<PointerEnabler>();
        //!!!!!!!!!!!
        movement = FindObjectOfType<CharacterMovement>();
        powers = FindObjectOfType<Powers>();
        if (Army.army != null) { 
        if (Army.army.enabled == true)
        {
            if (Army.army.currentFighter != null) { Army.army.SummonRobot(); }
        }
        }

        pointer.disablePlayerControl = true;

        if (powers != null)
        {
            powers.enabled = false;
        }
        if (canvas != null && canvas.Count > 0)
        {
            foreach (Canvas canv in canvas)
            {
                if (canv != null)
                {
                    if (canv.transform.Find("StartSphereMesh") != null)
                    {
                        canv.transform.Find("StartSphereMesh").gameObject.SetActive(false);
                    }
                    canv.enabled = false;
                }

            }
        }
        if (extras != null) { extras.SetActive(false); }


        if (alsoMovement)
        {
            movement.StopMovement();

        }
    }

    private void OnDisable()
    {
        pointer.disablePlayerControl = false;

        //enablesspowers
        if (powers != null)
        {
            powers.enabled = true;
        }

        if (canvas != null && canvas.Count > 0)
        {
            foreach (Canvas canv in canvas)
            {
                if (canv != null)
                {
                    if (canv.transform.Find("StartSphereMesh") != null)
                    {
                        canv.transform.Find("StartSphereMesh").gameObject.SetActive(true);
                    }
                    canv.enabled = true;

                }
            }
        }
        if (extras != null) { extras.SetActive(true); }

        if (alsoMovement)
        {
            movement.AbleToMove();
        }
    }

}
