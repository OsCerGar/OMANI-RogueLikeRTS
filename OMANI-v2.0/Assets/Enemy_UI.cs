using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_UI : NPC_UI {

    public override void LateUpdate()
    {
        lifeClock.fillAmount = (float)(npc.life / npc.startLife);
        lifeClock.transform.rotation = fixedRotation;

    }
}
