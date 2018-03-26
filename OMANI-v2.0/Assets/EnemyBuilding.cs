using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilding : NPC
{
    
        void Awake()
        {
            boyType = "Building";
        }
        public override void Update()
        {
            if (state == "Alive")
            {
                if (life <= 0)
                {
                    //provisional :D
                    Die();
                    state = "Dead";
                }
            }
        }

    }