using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
using System;

public class exPlicativoTreeControler : MonoBehaviour {
         [SerializeField] GameObject[] holograms; 

       public delegate void ActivateTutorial(Transform posToGo,string whatToShow);
       public static event ActivateTutorial Tutorial;//un evento para cada fragmento del tutorial

    [SerializeField] BehaviorTree TutorialBehaviour;
    
        void OnEnable()
        {
        Tutorial += ActivateMovementTut;
        }


        void OnDisable()
        {
        Tutorial -= ActivateMovementTut;
        }


        void ActivateMovementTut(Transform posToGo, string whatToShow)
        {
            ClearHolograms();
        foreach (var hologram in holograms)
        {
            if (hologram.name == whatToShow)
            {
                //gets the Vars
                TutorialBehaviour.enabled = true;
                var posToGoVar = (SharedTransform)TutorialBehaviour.GetVariable("posToGo");
                posToGoVar.Value = posToGo;
                var hologramVar = (SharedGameObject)TutorialBehaviour.GetVariable("hologram");
                hologramVar.Value = hologram;
            }
        }
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
