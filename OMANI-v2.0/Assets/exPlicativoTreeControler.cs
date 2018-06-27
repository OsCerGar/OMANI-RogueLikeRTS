using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;

public class exPlicativoTreeControler : MonoBehaviour
{
    [SerializeField] GameObject[] holograms;

    [SerializeField] BehaviorTree TutorialBehaviour, FollowBehaviour;

    public void ActivateMovementTut(GameObject posToGo, string whatToShow)
    {
        ClearHolograms();
        foreach (var hologram in holograms)
        {
            if (hologram.name.Equals(whatToShow))
            {
                //gets the Vars
                FollowBehaviour.enabled = false;
                TutorialBehaviour.enabled = true;
                var posToGoVar = (SharedGameObject)TutorialBehaviour.GetVariable("posToGo");
                posToGoVar.Value = posToGo;
                var hologramVar = (SharedGameObject)TutorialBehaviour.GetVariable("hologram");
                hologramVar.Value = hologram;
            }
        }
    }
    public void ActivateFollow()
    {
        TutorialBehaviour.enabled = false;
        FollowBehaviour.enabled = true;
    }

    private void ClearHolograms()
    {
        foreach (var hologram in holograms)
        {
            //gets the Vars
            hologram.SetActive(false);

        }
    }

}
