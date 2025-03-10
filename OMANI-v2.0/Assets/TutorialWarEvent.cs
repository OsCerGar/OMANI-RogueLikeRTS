﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWarEvent : MonoBehaviour {
    NPC[] npcArrays;
    GameObject Player;
	// Use this for initialization
	void Start () {
        npcArrays = GetComponentsInChildren<NPC>();
        foreach (var item in npcArrays)
        {
            item.Damage = 0;
            Player = GameObject.Find("Player");
            
        }
    }

    public void ActivateDeath()
    {
        foreach (var item in npcArrays)
        {
            if (item.GetType() ==  typeof(Enemy))
            {
                item.Damage = 20;
            }
        }
    }
}
