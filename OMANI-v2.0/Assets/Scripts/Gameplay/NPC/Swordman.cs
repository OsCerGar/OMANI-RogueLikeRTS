using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : NPC
{
    void Awake()
    {
        boyType = "Swordsman";
    }
    public override void Update()
    {
        base.Update();
        checkVariables();
    }
}
