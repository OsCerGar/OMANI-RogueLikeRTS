﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_War : MonoBehaviour
{

    float recluteNumbers;
    [SerializeField]
    List<NPC> listOfNPC;

    GameObject warObjective;
    exPlicativoTreeControler masterWorker;
    LookDirectionsAndOrder lookDirections;
    TutorialWarEvent warEvent;
    Player player;

    bool reclute = false;
    /*
    private void OnEnable()
    {
       // Player.playerDead += Fade;
    }

    private void OnDisable()
    {
        //Player.playerDead -= Fade;
    }

    private void Start()
    {
        warObjective = this.transform.GetChild(0).gameObject;
        lookDirections = FindObjectOfType<LookDirectionsAndOrder>();
        warEvent = FindObjectOfType<TutorialWarEvent>();
        player = FindObjectOfType<Player>();
    }

    private void Fade()
    {
        Initiate.Fade("Tutorial2", Color.red, 3f);
    }

    // Update is called once per frame
    void Update()
    {


        if (reclute == false)
        {
            foreach (NPC npc in listOfNPC)
            {
                if (npc.AI_GetTarget() != null)
                {
                    recluteNumbers++;
                }
            }

            if (recluteNumbers > 2)
            {
                masterWorker = FindObjectOfType<exPlicativoTreeControler>();

                if (lookDirections.playingOnController)
                {
                    masterWorker.ActivateMovementTut(warObjective, "RightStick+R1");
                }
                else
                {
                    masterWorker.ActivateMovementTut(warObjective, "RightClick");
                }
                reclute = true;
                recluteNumbers = 0;
            }
            else { recluteNumbers = 0; }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 5") || Input.GetMouseButtonDown(1))
            {
                recluteNumbers++;
            }
            if (recluteNumbers > 2)
            {
                warEvent.ActivateDeath();
                Debug.Log("Activate Death");
            }
        }
    }
    */
}
